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

    public class Responsavel  {

        private string id;
        private string nome;
        private string status;
        private string motivo;
        private Cliente clienteRestrito;
        private string clienteRestritoId;
        private Cliente clienteDefault;
        private string clienteDefaultId;
        private string senha;
        private string eMail;
        private DateTime edicao;
        private Byte[] rowVersion;


        string[] tabStatus = { "Ativo", "Inativo" };
        string[] tabMotivos = { "Ativo", "Bloqueado", "Cancelado" };


        [Key]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Identificação do responsável deve ser informado.")]
        [StringLength(20, ErrorMessage = "Identificação do responsável com tamanho maior que o permitido (20).")]
        [RegularExpression(@"^[A-Z0-9][A-Za-z0-9]{0,19}$", ErrorMessage = "Identificação do responsável com formação inválida.")]
        public string Id {
            get { return id; }
            set { if (ConsistirId(value)) id = value; else { throw new Exception("Erro."); } }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Nome do responsável deve ser informado.")]
        [StringLength(256, ErrorMessage = "Nome do responsável com tamanho maior que o permitido (256).")]
        [RegularExpression(@"^[A-Z][A-Za-z0-9áéíóúÁÉÍÓÚçâõÃÕêÊ,. ]{0,255}$", ErrorMessage = "Nome do responsável com formação inválida.")]
        public string Nome {
            get { return nome; }
            set { if (ConsistirNome(value)) nome = value; else { throw new Exception("Erro Nome."); } }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Status do responsável deve ser informado.")]
        [StringLength(30, ErrorMessage = "Status do responsável com tamanho maior que o permitido (30).")]
        [RegularExpression(@"Ativo|Inativo|Bloqueado", ErrorMessage = "Status do responsável com formação inválida.")]
        public string Status {
            get { return status; }
            set { if (ConsistirStatus(value)) status = value; else { throw new Exception("Erro Status."); } }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Motivo do status do responsável deve ser informado.")]
        [StringLength(30, ErrorMessage = "Motivo do status do responsável com tamanho maior que o permitido (30).")]
        [RegularExpression(@"Ativo|Bloqueado|Cancelado", ErrorMessage = "Motivo do status do responsável com formação inválida.")]
        public string Motivo {
            get { return motivo; }
            set { if (ConsistirMotivo(value)) motivo = value; else { throw new Exception("Erro Motivo."); } }
        }

        [StringLength(20, ErrorMessage = "Id do cliente restrito com tamanho maior que o permitido (20).")]
        public String ClienteRestritoId {
            get { return clienteRestritoId; }
            set { if (ConsistirClienteRestritoId(value)) clienteRestritoId = value; else  throw new Exception("Erro Cliente Restrito."); }
        }

        public virtual Cliente ClienteRestrito {
            get { return clienteRestrito; }
            set { if (ConsistirClienteRestrito(value)) clienteRestrito = value; else throw new Exception("Erro Cliente Restrito."); }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Cliente default do responsável deve ser informado.")]
        [StringLength(20, ErrorMessage = "Id do cliente default do resposável com tamanho maior que o permitido (20).")]
        public String ClienteDefaultId { 
            get {return clienteDefaultId; }
            set { if (ConsistirClienteDefaultId(value)) clienteDefaultId = value; else throw new Exception("Erro Cliente Default Id."); }
        }

        public virtual Cliente ClienteDefault {
            get { return clienteDefault; }
            set { if (ConsistirClienteDefault(value)) clienteDefault = value; else throw new Exception("Erro Cliente Default."); }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Senha do responsável deve ser informada.")]
        [StringLength(20, ErrorMessage = "Senha do responsável com tamanho maior que o permitido (20).")]
        [RegularExpression(@"^\w{4,20}$", ErrorMessage = "Senha do responsável com formação inválida.")]
        public string Senha {
            get { return senha; }
            set { if (ConsistirSenha(value)) senha = value; else throw new Exception("Erro Senha."); }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "EMail do responsável deve ser informada.")]
        [StringLength(256, ErrorMessage = "EMail do responsável com tamanho maior que o permitido (256).")]
        [RegularExpression(@"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
            @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))", ErrorMessage = "EMail do responsável com formação inválida.")]
        public string EMail {
            get { return eMail; }
            set { if (ConsistirEMail(value)) eMail = value; else throw new Exception("Erro EMail."); } 
        }

        [DataType(DataType.DateTime)]
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


        public virtual ICollection<Agenda> Agendas { get; set; }
        public virtual ICollection<Atendimento> Atendimentos { get; set; }
        public virtual ICollection<Diario> Diarios { get; set; }
        public virtual ICollection<ParametroResponsavel> ParametrosResponsaveis { get; set; }
        public virtual ICollection<Pendencia> PendenciasAberturas { get; set; }
        public virtual ICollection<Pendencia> PendenciasAtuais { get; set; }
        public virtual ICollection<PendenciaHistorico> PendenciasHistoricos { get; set; }

        
        public bool ConsistirId(string xValue) {
            bool correto = true;
            string expressao = @"^[A-Za-z0-9][A-Za-z0-9 ]{0,19}$";
            if (String.IsNullOrEmpty(xValue))
                return false;
            if (!Regex.Match(xValue, expressao).Success) {
                return false;
            }
            return correto;
        }
        public bool ConsistirNome(string xValue) {
            bool correto = true;
            string expressao = @"^[A-Z][A-Za-z0-1áéíóúÁÉÍÓÚçâõÃÕêÊ,. ]{0,255}$";
            if (String.IsNullOrEmpty(xValue))
                return false;
            if (!Regex.Match(xValue, expressao).Success) {
                return false;
            }
            return correto;
        }
        public bool ConsistirStatus(string xValue) {
            bool correto = false;
            for (int ix = 0; ix < tabStatus.Length; ix++) {
                if (xValue.Trim().Equals(tabStatus[ix])) {
                    correto = true;
                }
            }
            return correto;
        }
        public bool ConsistirMotivo(string xValue) {
            bool correto = false;
            for (int ix = 0; ix < tabMotivos.Length; ix++) {
                if (xValue.Trim().Equals(tabMotivos[ix])) {
                    correto = true;
                }
            }
            return correto;
        }
        public bool ConsistirStatusMotivo(string xValueStatus, string xValueMotivo) {
            bool correto = false;
            if ((xValueStatus.Equals("Ativo") && xValueMotivo.Equals("Ativo")
            || (!xValueStatus.Equals("Ativo") && !xValueMotivo.Equals("Ativo")))) {
                correto = true;
            }
            return correto;
        }
        public bool ConsistirClienteRestritoId(string x) {
            bool correto = true;
            if (x != null) {
                var db = new Contexto();
                if (db.Clientes.FirstOrDefault(p => p.Id == x) == null)
                    correto = false;
            }
            return correto;
        }
        public bool ConsistirClienteRestrito(Cliente x) {
            bool correto = true;
            if (x != null) {
                var db = new Contexto();
                if (db.Clientes.FirstOrDefault(p => p.Id == x.Id) == null)
                    correto = false;
            }
            return correto;
        }
        public bool ConsistirClienteDefaultId(string x) {
            bool correto = true;
            var db = new Contexto();
            if (db.Clientes.FirstOrDefault(p => p.Id == x) == null)
                correto = false;
            return correto;
        }
        public bool ConsistirClienteDefault(Cliente x) {
            bool correto = true;
            var db = new Contexto();
            if (db.Clientes.FirstOrDefault(p => p.Id == x.Id) == null)
                correto = false;
            return correto;
        }
        public bool ConsistirSenha(string xValue) {
            bool correto = true;
            string expressao = @"^\w{4,20}$";
            if (String.IsNullOrEmpty(xValue))
                return false;
            if (!Regex.Match(xValue, expressao).Success) {
                return false;
            }
            return correto;
        }
        public bool ConsistirEMail(string xValue) {
            bool correto = true;
            string expressao = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))";
            if (String.IsNullOrEmpty(xValue))
                return false;
            if (!Regex.Match(xValue, expressao).Success) {
                return false;
            }
            return correto;
        }

    }
}


