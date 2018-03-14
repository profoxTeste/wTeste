using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WChat {

    public class Agenda   {

        private int id;
        private DateTime data;
        private string clienteId;
        private Cliente cliente;
        private string responsavelId;
        private Responsavel responsavel;
        private string detalhamento;
        private DateTime edicao;
        private Byte[] rowVersion;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Identificação da agenda deve ser informada.")]
        public int Id {
            get { return id; }
            set { if (ConsistirId(value)) id = value; else { throw new Exception("Erro Id."); } }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Data da agenda deve ser informada.")]
        public DateTime Data {
            get { return data; }
            set { if (ConsistirData(value)) data = value; else { throw new Exception("Erro Data."); } }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Cliente da agenda deve ser informado.")]
        [StringLength(20, ErrorMessage = "Id do cliente da agenda com tamanho maior que o permitido (20).")]
        public String ClienteId {
            get { return clienteId; }
            set { if (ConsistirClienteId(value)) clienteId = value; else { throw new Exception("Erro Cliente Id."); } }
        }

        public Cliente Cliente {
            get { return cliente; }
            set { if (ConsistirCliente(value)) cliente = value; else { throw new Exception("Erro Cliente."); } }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Responsável da agenda deve ser informado.")]
        [StringLength(20, ErrorMessage = "Id do responsável da agenda com tamanho maior que o permitido (20).")]
        public String ResponsavelId {
            get { return responsavelId; }
            set { if (ConsistirResponsavelId(value)) responsavelId = value; else { throw new Exception("Erro Resposável Id."); } }
        }

        public Responsavel Responsavel {
            get { return responsavel; }
            set { if (ConsistirResponsavel(value)) responsavel = value; else { throw new Exception("Erro Responsavel."); } }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Detalhamento da agenda deve ser informado.")]
        [StringLength(5000, ErrorMessage = "Detalhamento da agenda com tamanho maior que o permitido (5000).")]
        [RegularExpression(@"^[A-Z][A-Za-z0-9áéíóúÁÉÍÓÚçâõÃÕêÊ,. ]{0,4999}$", ErrorMessage = "Detalhamento da agenda com formação inválida.")]
        public string Detalhamento {
            get { return detalhamento; }
            set { if (ConsistirDetalhamento(value)) detalhamento = value; else { throw new Exception("Erro Detalhamento."); } }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Data de edição deve ser informada.")]
        public DateTime Edicao {
            get { return edicao; }
            set { edicao = value; }
        }

        [Timestamp]
        public Byte[] RowVersion {
            get { return rowVersion; }
            set { rowVersion = value; }
        }


        public bool ConsistirId(int xValue) {
            bool correto = true;
            if (xValue < 1)
                return false;
            return correto;
        }
        public bool ConsistirClienteId(string xValue) {
            bool correto = true;
            var db = new Contexto();
            if  (db.Clientes.FirstOrDefault(p => p.Id == xValue) == null)
                correto = false;
            return correto;
        }
        public bool ConsistirCliente(Cliente xValue) {
            bool correto = true;
            var db = new Contexto();
            if  (db.Clientes.FirstOrDefault(p => p.Id == xValue.Id) == null)
                correto = false;
            return correto;
        }
        public bool ConsistirResponsavelId(string xValue) {
            bool correto = true;
            var db = new Contexto();
            if  (db.Responsaveis.FirstOrDefault(p => p.Id == xValue) == null)
                correto = false;
            return correto;
        }
        public bool ConsistirResponsavel(Responsavel xValue) {
            bool correto = true;
            var db = new Contexto();
            if (db.Responsaveis.FirstOrDefault(p => p.Id == xValue.Id) == null)
                correto = false;
            return correto;
        }
        public bool ConsistirData(DateTime xValue) {
            bool correto = true;
            return correto;
        }
        public bool ConsistirDetalhamento(string xValue) {
            bool correto = true;
            string expressao = @"^[A-Z][A-Za-z0-9áéíóúÁÉÍÓÚçãõÃÕêÊâÂ,. ]{0,4999}$";
            if (String.IsNullOrEmpty(xValue))
                return false;
            if (!Regex.Match(xValue, expressao).Success) {
                return false;
            }
            return correto;
        }

    }
}
