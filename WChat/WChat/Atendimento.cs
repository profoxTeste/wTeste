using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WChat
{
    public class Atendimento {

        private int id;
        private DateTime data;
        private string clienteId;
        private Cliente cliente;
        private string contato;
        private string responsavelId;
        private Responsavel responsavel;
        private int duracao;
        private string assuntoId;
        private Assunto assunto;
        private string motivador;
        private string detalhamento;
        private DateTime edicao;
        private Byte[] rowVersion;
        
        string[] tabMotivadores = { "Cliente", "Profox" };
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Identificação do atendimento deve ser informada.")]
        public int Id {
            get { return id; }
            set { if (ConsistirId(value)) id = value; else throw new Exception("Erro Id."); }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Data do atendimento deve ser informada.")]
        public DateTime Data {
            get { return data; }
            set { if (ConsistirData(value)) data = value; else throw new Exception("Erro Data."); }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Id do Cliente do atendimento deve ser informado.")]
        [StringLength(20, ErrorMessage = "Id do cliente do atendimento com tamanho maior que o permitido (20).")]
        public String ClienteId {
            get { return clienteId; }
            set { if (ConsistirClienteId(value)) clienteId = value; else throw new Exception("Erro Cliente Id."); }
        }

        public virtual Cliente Cliente {
            get { return cliente; }
            set { if (ConsistirCliente(value)) cliente = value; else throw new Exception("Erro Cliente."); }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Contato do Atendimento deve ser infoemado.")]
        [StringLength(20, ErrorMessage = "Contato do atendimento com tamanho maior que o permitido (20).")]
        [RegularExpression(@"^[A-Z][A-Za-z0-9áéíóúÁÉÍÓÚÇçãõÃÕêÊ,. ]{0,19}$", ErrorMessage = "Detalhamento da agenda com formação inválida.")]
        public string Contato {
            get { return contato; }
            set { if (ConsistirContato(value)) contato = value; else throw new Exception("Erro Contato."); }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Responsável do atendimento deve ser informado.")]
        [StringLength(20, ErrorMessage = "Id do responsável do atendimento com tamanho maior que o permitido (20).")]
        public String ResponsavelId { 
            get { return responsavelId; }
            set { if (ConsistirResponsavelId(value)) responsavelId = value; else throw new Exception("Erro Responsável Id."); }
        }

        public Responsavel Responsavel {
            get { return responsavel; }
            set { if (ConsistirResponsavel(value)) responsavel = value; else throw new Exception("Erro Responsável."); }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Duração do atendimento deve ser informado.")]
        [Range(1, 1440, ErrorMessage = "Duração do atendimento fora do intervalo 1 a 1440.")]
        public int Duracao {
            get { return duracao; }
            set { if (ConsistirDuracao(value)) duracao = value; else throw new Exception("Erro Duração."); }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Id do assunto do atendimento deve ser informado.")]
        [StringLength(20, ErrorMessage = "Id do assunto do atendimento com tamanho maior que o permitido (20).")]
        public String AssuntoId { 
            get { return assuntoId; }
            set { if (ConsistirAssuntoId(value)) assuntoId = value; else throw new Exception("Erro Assunto Id."); }
        }

        public Assunto Assunto { 
            get { return assunto; }
            set { if (ConsistirAssunto(value)) assunto = value; else throw new Exception("Erro Assunto."); }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Motivador do atendimento deve ser informado.")]
        [StringLength(10, ErrorMessage = "Motivador do atendimento com tamanho maior que o permitido (10).")]
        [RegularExpression(@"Profox|Cliente", ErrorMessage = "Motivador do atendimento com formação inválida.")]
        public string Motivador { 
            get { return motivador; }
            set { if (ConsistirMotivador(value)) motivador = value; else throw new Exception("Erro Motivador."); }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Detalhamento {0} deve ser informado.")]
        [StringLength(5000, ErrorMessage = "Detalhamento {0} com tamanho maior que o permitido (5000).")]
        [RegularExpression(@"^[A-Z][A-Za-z0-9áéíóúÁÉÍÓÚçâõÃÕêÊ,. ]{0,4999}$", ErrorMessage = "Detalhamento {0} com formação inválida.")]
        public string Detalhamento { 
            get { return detalhamento; }
            set { if (ConsistirDetalhamento(value)) detalhamento = value; else throw new Exception("Erro Detalhamento."); }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Edição deve ser informado.")]
        public DateTime Edicao { 
            get { return edicao; }
            set { edicao = value; }
        }

        [Timestamp]
        public Byte[] RowVersion { 
            get { return rowVersion; }
            set { rowVersion = value; }
        }


        
        
        
        public bool ConsistirId(int x) {
            bool correto = true;
            if (x < 1)
                return false;
            return correto;
        }
        public bool ConsistirData(DateTime x) {
            bool correto = true;
            return correto;
        }
        public bool ConsistirClienteId(string x) {
            bool correto = true;
            var db = new Contexto();
            if (db.Clientes.FirstOrDefault(p => p.Id == x) == null)
                correto = false;
            return correto;
        }
        public bool ConsistirCliente(Cliente x) {
            bool correto = true;
            var db = new Contexto();
            if  (db.Clientes.FirstOrDefault(p => p.Id == x.Id) == null)
                correto = false;
            return correto;
        }
        public bool ConsistirContato(string xValue) {
            bool correto = true;
            string expressao = @"^[A-Z][A-Za-z0-9áéíóúÁÉÍÓÚçãõÃÕêÊ,. ]{0,19}$";
            if (String.IsNullOrEmpty(xValue))
                return false;
            if (!Regex.Match(xValue, expressao).Success) {
                return false;
            }
            return correto;
        }
        public bool ConsistirResponsavelId(string x) {
            bool correto = true;
            var db = new Contexto();
            if (db.Responsaveis.FirstOrDefault(p => p.Id == x)  == null)
                correto = false;
            return correto;
        }
        public bool ConsistirResponsavel(Responsavel x) {
            bool correto = true;
            var db = new Contexto();
            if (db.Responsaveis.FirstOrDefault(p => p.Id == x.Id) == null)
                correto = false;
            return correto;
        }
        public bool ConsistirDuracao(int x) {
            bool correto = true;
            if (x < 1)
                return false;
            return correto;
        }
        public bool ConsistirAssuntoId(string x) {
            bool correto = true;
            var db = new Contexto();
            if (db.Assuntos.FirstOrDefault(p => p.Id == x) == null)
                correto = false;
            return correto;
        }
        public bool ConsistirAssunto(Assunto x) {
            bool correto = true;
            var db = new Contexto();
            if (db.Assuntos.FirstOrDefault(p => p.Id == x.Id) == null)
                correto = false;
            return correto;
        }
        public bool ConsistirMotivador(string xValue) {
            bool correto = false;
            for (int ix = 0; ix < tabMotivadores.Length; ix++) {
                if (xValue.Trim().Equals(tabMotivadores[ix])) {
                    correto = true;
                }
            }
            return correto;
        }
        public bool ConsistirDetalhamento(string xValue) {
            bool correto = true;
            string expressao = @"^[A-Z][A-Za-z0-9áéíóúÁÉÍÓÚçâõÃÕêÊ,. ]{0,4999}$";
            if (String.IsNullOrEmpty(xValue))
                return false;
            if (!Regex.Match(xValue, expressao).Success) {
                return false;
            }
            return correto;
        }
    
    }
}
