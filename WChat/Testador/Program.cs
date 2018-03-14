using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.Data.Entity;
using System.Text.RegularExpressions;
using WChat;
using System.Data.Entity.Validation;

namespace WChat
{
    static class Program
    {

        [STAThread]
        static void Main() {
            string escolha;
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<Contexto, WChat.Migrations.Configuration>());
            // Estudar melhor

            try {
                Console.WriteLine("Tecle 1 para assuntos.");
                Console.WriteLine("Tecle 2 para clientes.");
                Console.WriteLine("Tecle 3 para responsáveis.");
                Console.WriteLine("Tecle 4 para agenda.");
                Console.WriteLine("Tecle 5 para atendimento.");
                Console.WriteLine("Tecle 6 para pendência.");
                Console.WriteLine("Tecle R para Regex.");
                Console.WriteLine("Tecle X para terminar.");
                escolha = Console.ReadLine().ToUpper();

                while (escolha != "X") {
                    if (escolha == "1") RotAssuntos(); 
                    if (escolha == "2") RotClientes();
                    if (escolha == "3") RotResponsaveis();
                    if (escolha == "4") RotAgenda();
                    if (escolha == "5") RotAtendimento();
                    if (escolha == "6") RotPendencia();
                    if (escolha == "R") RotRegex(); 
                    Console.WriteLine("Pressione qualquer tecla para sair...");
                    Console.ReadKey();
                    escolha = "X";
                }
            }
            catch (DbEntityValidationException e) {
                foreach (var eve in e.EntityValidationErrors) {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors) {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                Console.WriteLine("Pressione qualquer tecla para sair...");
                Console.ReadKey();
                escolha = "X";
            }
            catch (Exception e) { 
                Console.WriteLine(e.Message); 
                Console.ReadKey(); }
        }

        static public void RotClientes() {
            var db = new Contexto();
            db.Database.CreateIfNotExists();

                        Cliente cli = db.Cliente.FirstOrDefault(p => p.Id == "Cli1");
                        if (cli == null) {
                            db.Cliente.Add(new Cliente() {
                                Id = "Cli1",
                                Nome = "Cliente Um",
                                Telefone = "22224455",
                                Contato = "Zé",
                                Detalhamento = "Detalhes Um",
                                Status = "Ativo",
                                Motivo = "Ativo",
                                Razao = "Razao Um",
                                Dias = 10,
                                Qualidade = 10,
                                CentralizadoraId = null,
                                Edicao = DateTime.Parse("2016-01-01 12:12:12.1234")
                            });
                        }
                        else {
                            cli.Nome = "AAAAAAAAAA";
                        }
                        db.SaveChanges();

                        cli = db.Cliente.FirstOrDefault(p => p.Id == "Cli2");
                        if (cli == null) {
                            db.Cliente.Add(new Cliente() {
                                Id = "Cli2",
                                Nome = "Cliente Dois",
                                Telefone = "22224455",
                                Contato = "Zé",
                                Detalhamento = "Detalhes Dois",
                                Status = "Ativo",
                                Motivo = "Ativo",
                                Razao = "Razao Um",
                                Dias = 10,
                                Qualidade = 10,
                                CentralizadoraId = "Cli1",
                                Edicao = DateTime.Parse("2016-01-01 12:12:12.1234")
                            });
                        }
                        else {
                            cli.CentralizadoraId = "Cli1";
                        }
                        cli = db.Cliente.FirstOrDefault(p => p.Id == "Cli3");
                        if (cli == null) {
                            db.Cliente.Add(new Cliente() {
                                Id = "Cli3",
                                Nome = "Cliente Tres",
                                Telefone = "22224455",
                                Contato = "Zé",
                                Detalhamento = "Detalhes Tres",
                                Status = "Ativo",
                                Motivo = "Ativo",
                                Razao = "Razao Tres",
                                Dias = 10,
                                Qualidade = 10,
                                CentralizadoraId = null,
                                Edicao = DateTime.Parse("2016-01-01 12:12:12.1234")
                            });
                        }
                        cli = db.Cliente.FirstOrDefault(p => p.Id == "Cli4");
                        if (cli == null) {
                            db.Cliente.Add(new Cliente() {
                                Id = "Cli4",
                                Nome = "Cliente Quatro",
                                Telefone = "22224455",
                                Contato = "Zé",
                                Detalhamento = "Detalhes Quatro",
                                Status = "Ativo",
                                Motivo = "Ativo",
                                Razao = "Razao Quatro",
                                Dias = 10,
                                Qualidade = 10,
                                CentralizadoraId = null,
                                Edicao = DateTime.Parse("2016-01-01 12:12:12.1234")
                            });
                        }
                        
                        db.SaveChanges();

                        var dados = from a in db.Cliente select a;

                        foreach (var linha in dados) {
                            Console.WriteLine("{0, -20} - {1} - {2}", linha.Id, linha.Nome, linha.Telefone);
                        }
                    }
        static public void RotResponsaveis() {
                        var db = new Contexto();
                        db.Database.CreateIfNotExists();

                        Responsavel res = db.Responsavel.FirstOrDefault(p => p.Id == "ResUm");
                        if (res == null) {
                            db.Responsavel.Add(new Responsavel() {
                                Id = "ResUm",
                                Nome = "Responsavel Um",
                                Status = "Ativo",
                                Motivo = "Ativo",
                                ClienteRestritoId = null,
                                ClienteDefaultId = "Cli1",
                                Senha = "12345678",
                                EMail = "profox@profox.com.br",
                                Edicao = DateTime.Now.Date
                            });
                        }
                        else {
                            res.Nome = "AAAAAAAAAA";
                        }
                        res = db.Responsavel.FirstOrDefault(p => p.Id == "ResDois");
                        if (res == null) {
                            db.Responsavel.Add(new Responsavel() {
                                Id = "ResDois",
                                Nome = "Responsavel Dois",
                                Status = "Ativo",
                                Motivo = "Ativo",
                                ClienteRestritoId = null,
                                ClienteDefaultId = "Cli1",
                                Senha = "12345678",
                                EMail = "profox@profox.com.br",
                                Edicao = DateTime.Now.Date
                            });
                        }
                        res = db.Responsavel.FirstOrDefault(p => p.Id == "ResTres");
                        if (res == null) {
                            db.Responsavel.Add(new Responsavel() {
                                Id = "ResTres",
                                Nome = "Responsavel Tres",
                                Status = "Ativo",
                                Motivo = "Ativo",
                                ClienteRestritoId = null,
                                ClienteDefaultId = "Cli1",
                                Senha = "12345678",
                                EMail = "profox@profox.com.br",
                                Edicao = DateTime.Now.Date
                            });
                        }
                        res = db.Responsavel.FirstOrDefault(p => p.Id == "ResQuatro");
                        if (res == null) {
                            db.Responsavel.Add(new Responsavel() {
                                Id = "ResQuatro",
                                Nome = "Responsavel Quatro",
                                Status = "Ativo",
                                Motivo = "Ativo",
                                ClienteRestritoId = null,
                                ClienteDefaultId = "Cli1",
                                Senha = "12345678",
                                EMail = "profox@profox.com.br",
                                Edicao = DateTime.Now.Date
                            });
                        }
                        
                        db.SaveChanges();

                        var dados = from a in db.Responsavel select a;

                        foreach (var linha in dados) {
                            Console.WriteLine("{0, -20} - {1} - {2}", linha.Id, linha.Nome, linha.Status);
                        }
                    }
        static public void RotAssuntos() {
                        var db = new Contexto();
                        db.Database.CreateIfNotExists();

                        Assunto ass = db.Assunto.FirstOrDefault(p => p.Id == "AssUm");
                        if (ass == null) {
                            db.Assunto.Add(new Assunto() {
                                Id = "AssUm",
                                Nome = "Assunto Um",
                                Edicao = DateTime.Parse("2016-01-01 12:12:12.1234")
                            });
                        }
                        else {
                            ass.Nome = "AAAAAAAAAA";
                        }
                        ass = db.Assunto.FirstOrDefault(p => p.Id == "AssDois");
                        if (ass == null) {
                            db.Assunto.Add(new Assunto() {
                                Id = "AssDois",
                                Nome = "Assunto Dois",
                                Edicao = DateTime.Parse("2016-01-01 12:12:12.1234")
                            });
                        }
                        ass = db.Assunto.FirstOrDefault(p => p.Id == "AssTres");
                        if (ass == null) {
                            db.Assunto.Add(new Assunto() {
                                Id = "AssTres",
                                Nome = "Assunto Tres",
                                Edicao = DateTime.Parse("2016-01-01 12:12:12.1234")
                            });
                        }
                        ass = db.Assunto.FirstOrDefault(p => p.Id == "AssQuatro");
                        if (ass == null) {
                            db.Assunto.Add(new Assunto() {
                                Id = "AssQuatro",
                                Nome = "Assunto Quatro",
                                Edicao = DateTime.Parse("2016-01-01 12:12:12.1234")
                            });
                        }
                        ass = db.Assunto.FirstOrDefault(p => p.Id == "AssCinco");
                        if (ass == null) {
                            db.Assunto.Add(new Assunto() {
                                Id = "AssCinco",
                                Nome = "Assunto Cinco",
                                Edicao = DateTime.Parse("2016-01-01 12:12:12.1234")
                            });
                        }

                        db.SaveChanges();

                        var dados = from a in db.Assunto select a;

                        foreach (var linha in dados) {
                            Console.WriteLine("{0, -20} - {1}", linha.Id, linha.Nome);
                        }
        }
        static public void RotAgenda() {
            var db = new Contexto();
            db.Database.CreateIfNotExists();

            Agenda ag = db.Agenda.FirstOrDefault(p => p.Id == 123);
            var agora = new DateTime();
            agora = DateTime.Now.Date;
            if (ag == null) {
                db.Agenda.Add(new Agenda() {
                    Id = 0,
                    Data = DateTime.Parse("2016-01-01 12:12:12.1234"),
                    ClienteId = "Cli1",
                    ResponsavelId = "ResUm",
                    Detalhamento = "Detalhes Um",
                    Edicao = DateTime.Parse("2016-01-01 12:12:12.1234"),
                });
            }
            else {
                ag.Detalhamento = "AAAAAAAAAA";
            }

            db.SaveChanges();

            var dados = from a in db.Agenda select a;

            foreach (var linha in dados) {
                Console.WriteLine("{0} - {1}", linha.Id, linha.Detalhamento);
            }
        }
        static public void RotAtendimento() {
            var db = new Contexto();
            db.Database.CreateIfNotExists();

            Atendimento at = db.Atendimento.FirstOrDefault(p => p.Id == 123);
            if (at == null) {
                db.Atendimento.Add(new Atendimento() {
                    Id = 0,
                    Data = DateTime.Parse("2016-01-01 12:12:12.1234"),
                    ClienteId = "Cli1",
                    ResponsavelId = "ResUm",
                    Contato = "Zé atendido",
                    Duracao = 5,
                    AssuntoId = "AssUm",
                    Motivador = "Profox",
                    Detalhamento = "Detalhes Um",
                    Edicao = DateTime.Parse("2016-01-01 12:12:12.1234"),
                });
            }
            else {
                at.Detalhamento = "AAAAAAAAAA";
            }

            db.SaveChanges();

            var dados = from a in db.Atendimento select a;

            foreach (var linha in dados) {
                Console.WriteLine("{0} - {1}", linha.Id, linha.Detalhamento);
            }
        }
        static public void RotPendencia() {
            var db = new Contexto();
            db.Database.CreateIfNotExists();

            var pen = db.Pendencia.FirstOrDefault(p => p.Id == 123);
            if (pen == null) {
                db.Pendencia.Add(new Pendencia() {
                    Id = 0,
                    ClienteId = "Cli1",
                    Contato = "Zé",
                    DataAbertura = DateTime.Parse("2016-01-01 12:12:12.1234"),
                    DataUltimaAtualizacao = DateTime.Parse("2016-01-01 12:12:12.1234"),
                    ResponsavelAberturaId = "ResUm",
                    ResponsavelAtualId = "ResUm",
                    Status = "Aberto",
                    Motivo = "Pronto",
                    AssuntoId = "AssUm",
                    DetalhamentoAbertura = "Detalhes Um",
                    DetalhamentoAtual = "Detalhes Um",
                    DetalhamentoFinal = "Detalhes Um",
                    Edicao = DateTime.Parse("2016-01-01 12:12:12.1234"),
                });
            }
            else {
                pen.DetalhamentoAtual = "AAAAAAAAAA";
            }

            db.SaveChanges();

            var dados = from a in db.Pendencia select a;

            foreach (var linha in dados) {
                Console.WriteLine("{0} - {1}", linha.Id, linha.DetalhamentoAtual);
            }
        }
        static public void RotRegex() {
            string nome = "Jose da Silva Pereira ";
            string expressao = @"^[A-Z]{1}[A-Za-z0-9áéíóúÁÉÍÓÚçâõÃÕêÊ,. ]{0,255}$";
            if (!Regex.Match(nome, expressao).Success) {
                Console.WriteLine("Deu erro...");
            }
            else { Console.WriteLine("Deu certo..."); }
        }
    }
}
/*
                1, 
                clienteId, 
                contato, 
                data, 
                data, 
                responsavelId, 
                responsavelId, 
                status, 
                motivo, 
                assuntoId, 
                detalhamento, 
                detalhamento, 
                detalhamento)) {
                this.id = 0;
                ClienteId = clienteId;
                Contato = contato;
                DataAbertura = data;
                DataUltimaAtualizacao = data;
                ResponsavelAtualId = responsavelId;
                ResponsavelAberturaId = responsavelId;
                Status = status;
                Motivo = motivo;
                AssuntoId = assuntoId;
                DetalhamentoAbertura = detalhamento;
                DetalhamentoAtual = detalhamento;
                DetalhamentoFinal = " ";
*/