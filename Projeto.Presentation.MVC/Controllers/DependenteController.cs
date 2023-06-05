using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Projeto.Presentation.MVC.Models;
using Projeto.Repository.Entities;
using Projeto.Repository.Repositories;
using System;
using System.Collections.Generic;

namespace Projeto.Presentation.MVC.Controllers
{
    public class DependenteController : Controller
    {        
        public IActionResult Cadastro([FromServices] ClienteRepository clienteRepository)
        {
            var result = GetDependenteCadastroModel(clienteRepository);
            return View(result);
        }

        [HttpPost]
        public IActionResult Cadastro(DependenteCadastroModel model, 
            [FromServices] DependenteRepository dependenteRepository,
            [FromServices] ClienteRepository clienteRepository)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var dependente = new Dependente();
                    dependente.Nome = model.Nome;
                    dependente.DataNascimento = DateTime.Parse(model.DataNascimento);

                    dependente.IdCliente = int.Parse(model.IdCliente);
                    dependenteRepository.Create(dependente);

                    TempData["MensagemSucesso"] = "Dependente cadastrado com sucesso. ";
                    ModelState.Clear();
                }
                catch (Exception ex)
                {
                    TempData["MensagemErro"] = "" + ex.Message;
                }               

            }            

            var result = GetDependenteCadastroModel(clienteRepository);
            return View(result);
        }

        public IActionResult Consulta(string nome, [FromServices] DependenteRepository dependenteRepository)
        {
            var dependentes = new List<Dependente>();

            try
            {
                dependentes = dependenteRepository.GetByNome(nome);
                TempData["Nome"] = nome;
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = "" + ex.Message;
            }
            return View(dependentes);
        }

        private DependenteCadastroModel GetDependenteCadastroModel(ClienteRepository clienteRepository)
        {
            var model = new DependenteCadastroModel();

            try
            {
                model.ListagemDeClientes = GetClientes(clienteRepository);              
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = "" + ex.Message;
            }
            return model;
        }

        public IActionResult Exclusao(int id, [FromServices] DependenteRepository dependenteRepository)
        {

            var dependente = dependenteRepository.GetById(id);

            try
            {
                if (dependente != null)
                {
                    dependenteRepository.Delete(dependente);
                    TempData["MensagemSucesso"] = "Dependente excluído com sucesso. ";
                }
                else
                {
                    throw new Exception("Dependente não encontrado. ");
                }
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = "" + ex.Message;
            }
            return RedirectToAction("Consulta");
        }

        private DependenteEdicaoModel GetDependenteEdicaoModel(ClienteRepository clienteRepository) 
        {
            var model = new DependenteEdicaoModel();
            try
            {
                model.ListagemDeClientes = GetClientes(clienteRepository);
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = "" + ex.Message;
            }
            return model;
        }

        public IActionResult Edicao(int id, [FromServices] DependenteRepository dependenteRepository, [FromServices] ClienteRepository clienteRepository) 
        {
            var model = GetDependenteEdicaoModel(clienteRepository);          

            try
            {
                var dependente = dependenteRepository.GetById(id);

                model.IdDependente = dependente.IdDependente;
                model.Nome = dependente.Nome;
                model.DataNascimento = dependente.DataNascimento.ToString("dd/MM/yyyy");
                model.IdCliente = dependente.IdCliente.ToString();
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = "" + ex.Message;
            }
            return View(model);
        }

        private List<SelectListItem> GetClientes(ClienteRepository clienteRepository) 
        {
            var listagemDeClientes = new List<SelectListItem>();

            foreach (var item in clienteRepository.GetAll())
            {
                    var opcao = new SelectListItem();
                    opcao.Value = item.IdCliente.ToString();
                    opcao.Text = item.Nome;
                    listagemDeClientes.Add(opcao); 
              
                
            }
            return listagemDeClientes;
        }

        [HttpPost]
        public IActionResult Edicao(DependenteEdicaoModel model,
            [FromServices] ClienteRepository clienteRepository,
           [FromServices] DependenteRepository dependenteRepository)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var dependente = new Dependente();

                    dependente.IdDependente = model.IdDependente;
                    dependente.Nome = model.Nome;
                    dependente.DataNascimento = DateTime.Parse(model.DataNascimento);
                    dependente.IdCliente = int.Parse(model.IdCliente);
                    dependenteRepository.Update(dependente);

                    TempData["MensagemSucesso"] = "Dependente atualizado com sucesso. ";
                    ModelState.Clear();
                }
                catch (Exception ex)
                {
                    TempData["MensagemErro"] = "" + ex.Message;
                }

            }

            var result = GetDependenteEdicaoModel(clienteRepository);
            return View(result);
        }
    }
}
