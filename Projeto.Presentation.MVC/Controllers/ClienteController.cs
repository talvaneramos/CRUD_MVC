using Microsoft.AspNetCore.Mvc;
using Projeto.Presentation.MVC.Models;
using Projeto.Repository.Entities;
using Projeto.Repository.Repositories;
using System;
using System.Collections.Generic;

namespace Projeto.Presentation.MVC.Controllers
{
    public class ClienteController : Controller
    {
        public IActionResult Cadastro()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastro(ClienteCadastroModel model, [FromServices] ClienteRepository clienteRepository)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (clienteRepository.GetByEmail(model.Email) != null)
                    {
                        throw new Exception("Email já cadastrado. Tente outro.");
                    }
                    else if (clienteRepository.GetByCpf(model.Cpf) != null)
                    {
                        throw new Exception("CPF já cadastrado. Tente outro");
                    }

                    var cliente = new Cliente();
                    cliente.Nome = model.Nome;
                    cliente.Email = model.Email;
                    cliente.Cpf = model.Cpf;

                    clienteRepository.Create(cliente);

                    TempData["MensagemSucesso"] = "Cliente cadastrado com sucesso";

                    ModelState.Clear();
                }
                catch (Exception ex)
                {
                    TempData["MensagemErro"] = ex.Message;
                }
            }
            return View();
        }

        //[HttpPost]
        public IActionResult Consulta(string nome, [FromServices] ClienteRepository clienteRepository)
        {
            var clientes = new List<Cliente>();

            try
            {
                clientes = clienteRepository.GetByNome(nome);
                TempData["Nome"] = nome;
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = "" + ex.Message;   
            }

            return View(clientes);
        }

        public IActionResult Exclusao(int id, [FromServices] ClienteRepository clienteRepository)
        {
            try
            {
                var cliente = clienteRepository.GetById(id);

                if(cliente != null)
                {
                    var quantidadeDependentes = clienteRepository.ContadorDependentes(cliente.IdCliente);

                    if (quantidadeDependentes == 0)
                    {
                        clienteRepository.Delete(cliente);
                        TempData["MensagemSucesso"] = "Cliente excluído com sucesso! ";
                    }
                    else
                    {
                        TempData["MensagemErro"] = $"Não é possível excluir o cliente selecionado "
                            + $" pois ele possui {quantidadeDependentes} dependentes ";
                    }                   
                }
                else
                {
                    throw new Exception("Cliente não encontrado! ");
                }

            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = "" + ex.Message;
            }
            return RedirectToAction("Consulta");
        }
        public IActionResult Edicao(int id, [FromServices] ClienteRepository clienteRepository)
        {
            var model = new ClienteEdicaoModel();

            try
            {                
                var cliente = clienteRepository.GetById(id);

                model.IdCliente = cliente.IdCliente;
                model.Nome = cliente.Nome;
                model.Email = cliente.Email;
                model.Cpf = cliente.Cpf;
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = "" + ex.Message;
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Edicao(ClienteEdicaoModel model, [FromServices] ClienteRepository clienteRepository)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var cliente = new Cliente();
                    cliente.IdCliente = model.IdCliente;
                    cliente.Nome = model.Nome;
                    cliente.Email = model.Email;
                    cliente.Cpf = model.Cpf;

                    clienteRepository.Update(cliente);
                    TempData["MensagemSucesso"] = "Cliente atualizado com sucesso. ";

                }
                catch (Exception ex)
                {
                    TempData["MensagemErro"] = "" + ex.Message;
                }
            }

            return RedirectToAction("Consulta");
        }

        

    }
}
