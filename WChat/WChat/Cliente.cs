using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WChat
{

    public class Cliente
    {

        private string id;
        private string nome;
        private string telefone;
        private string contato;
        private string detalhamento;
        private string status;
        private string motivo;
        private string razao;
        private int dias;
        private int qualidade;
        private Cliente centralizadora;
        private string centralizadoraId;
        private DateTime edicao;
        private Byte[] rowVersion;

        string[] tabStatus  = { "Ativo", "Inativo" };
        string[] tabMotivos = { "Ativo", "Bloqueado", "Cancelado" };


        [Key]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Identificação do cliente deve ser informado.")]
        [StringLength(20, ErrorMessage = "Identificação do cliente com tamanho maior que o permitido (20).")]
        [RegularExpression(@"^[A-Z0-9][A-Za-z0-9 ]{0,19}$", ErrorMessage = "Identificação do cliente com formação inválida.")]
        public string Id {
            get { return id; }
            set { if (ConsistirId(value)) id = value; else { throw new Exception("Erro."); } }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Nome do cliente deve ser informado.")]
        [StringLength(256, ErrorMessage = "Nome do cliente com tamanho maior que o permitido (256).")]
        [RegularExpression(@"^[A-Z][A-Za-z0-9áéíóúÁÉÍÓÚçâõÃÕêÊ,. ]{0,255}$", ErrorMessage = "Nome do cliente com formação inválida.")]
        public string Nome {
            get { return nome; }
            set { if (ConsistirNome(value)) nome = value; else { throw new Exception("Erro Nome."); } }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Telefone do cliente deve ser informado.")]
        [StringLength(60, ErrorMessage = "Telefone do cliente com tamanho maior que o permitido (60).")]
        [RegularExpression(@"^[0-9()][0-9(). ]{7,60}$", ErrorMessage = "Telefone do cliente com formação inválida.")]
        public string Telefone {
            get { return telefone; }
            set { if (ConsistirTelefone(value)) telefone = value; else { throw new Exception("Erro telefone."); } }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Contato do cliente deve ser informado.")]
        [StringLength(90, ErrorMessage = "Contato do cliente com tamanho maior que o permitido (90).")]
        [RegularExpression(@"^[A-Z][A-Za-z0-9áéíóúÁÉÍÓÚçâõÃÕêÊ,. ]{0,89}$", ErrorMessage = "Contato do cliente com formação inválida.")]
        public string Contato {
            get { return contato; }
            set { if (ConsistirContato(value)) contato = value; else { throw new Exception("Erro Contato."); } }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Detalhamento do cliente deve ser informado.")]
        [StringLength(5000, ErrorMessage = "Detalhamento do cliente com tamanho maior que o permitido (5000).")]
        [RegularExpression(@"^[A-Z][A-Za-z0-9áéíóúÁÉÍÓÚçâõÃÕêÊ,. ]{0,4999}$", ErrorMessage = "Detalhamento do cliente com formação inválida.")]
        public string Detalhamento {
            get { return detalhamento; }
            set { if (ConsistirDetalhamento(value)) detalhamento = value; else { throw new Exception("Erro Detalhamento."); } }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Status do cliente deve ser informado.")]
        [StringLength(30, ErrorMessage = "Status do cliente com tamanho maior que o permitido (30).")]
        [RegularExpression(@"Ativo|Inativo|Bloqueado", ErrorMessage = "Status do cliente com formação inválida.")]
        public string Status {
            get { return status; }
            set { if (ConsistirStatus(value)) status = value; 
                  else { throw new Exception("Erro Status."); } }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Motivo do status do cliente deve ser informado.")]
        [StringLength(30, ErrorMessage = "Motivo do status do cliente com tamanho maior que o permitido (30).")]
        [RegularExpression(@"Ativo|Bloqueado|Cancelado", ErrorMessage = "Motivo do status do cliente com formação inválida.")]
        public string Motivo {
            get { return motivo; }
            set { if (ConsistirMotivo(value)) motivo = value; else { throw new Exception("Erro Motivo."); } }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Razão do cliente deve ser informado.")]
        [StringLength(256, ErrorMessage = "Razão do cliente com tamanho maior que o permitido (256).")]
        [RegularExpression(@"^[A-Z][A-Za-z0-9áéíóúÁÉÍÓÚçâõÃÕêÊ,. ]{0,256}$", ErrorMessage = "Razão do cliente com formação inválida.")]
        public string Razao {
            get { return razao; }
            set { if (ConsistirRazao(value)) razao = value; else { throw new Exception("Erro Razão."); } }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Dias do cliente deve ser informado.")]
        [Range(1, 365, ErrorMessage = "Dias do cliente fora do intervalo 1 a 365.")]
        public int Dias {
            get { return dias; }
            set { if (ConsistirDias(value)) dias = value; else { throw new Exception("Erro Dias."); } }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Qualidade do cliente deve ser informado.")]
        [Range(1, 365, ErrorMessage = "Qualidade do cliente fora do intervalo 1 a 365.")]
        public int Qualidade {
            get { return qualidade; }
            set { if (ConsistirQualidade(value)) qualidade = value; else { throw new Exception("Erro Qualidade."); } }
        }

        [StringLength(20, ErrorMessage = "Id da centralizadora tamanho maior que o permitido (20).")]
        public String CentralizadoraId {
            get { return centralizadoraId; }
            set {
                if (value == null) {
                    centralizadoraId = null;
                    centralizadora = null;
                }
                else {
                    if (ConsistirCentralizadoraId(value)) {
                        centralizadoraId = value;
                    }
                    else {
                        throw new Exception("Erro Id Centralizadora.");
                    }
                }
            }
        }

        public Cliente Centralizadora {
            get { 
                return centralizadora; 
            }
            set {
                if (ConsistirCentralizadora(value)) {
                    if (value != null) {
                        centralizadora = value;
                        centralizadoraId = centralizadora.Id;
                    }
                    else {
                        centralizadora = null;
                        centralizadoraId = null;
                    }
                }
                else { 
                    throw new Exception("Erro Centralizadora."); 
                }
            }
        }

        //public int Nota { set; get; }

        public DateTime Edicao {
            get { return edicao; }
            set { edicao = DateTime.Parse("2016-01-01 12:12:12.1234"); }
        }

        [Timestamp]
        public Byte[] RowVersion {
            get { return rowVersion; }
            set { rowVersion = value; }
        }


        public virtual ICollection<Agenda> Agendas { get; set; }
        public virtual ICollection<Atendimento> Atendimentos { get; set; }
        public virtual ICollection<Pendencia> Pendencias { get; set; }
        public virtual ICollection<Cliente> Clientes { get; set; }
        public virtual ICollection<Responsavel> ResponsaveisDefaults { get; set; }
        public virtual ICollection<Responsavel> ResponsaveisRestritos { get; set; }



        public bool Consistir(
            string id,
            string nome,
            string telefone,
            string contato,
            string detalhamento,
            string status,
            string motivo,
            string razao,
            int dias,
            int qualidade,
            string centalizadoraId) {
            bool correto = true;
            if (correto) { correto = ConsistirId(id); }
            if (correto) { correto = ConsistirNome(nome); }
            if (correto) { correto = ConsistirTelefone(telefone); }
            if (correto) { correto = ConsistirContato(contato); }
            if (correto) { correto = ConsistirDetalhamento(detalhamento); }
            if (correto) { correto = ConsistirStatus(status); }
            if (correto) { correto = ConsistirMotivo(motivo); }
            if (correto) { correto = ConsistirStatusMotivo(status, motivo); }
            if (correto) { correto = ConsistirRazao(razao); }
            if (correto) { correto = ConsistirDias(dias); }
            if (correto) { correto = ConsistirQualidade(qualidade); }
            if (correto) { correto = ConsistirCentralizadoraId(centralizadoraId); }
            return correto;
        }
        public bool ConsistirId(string xValue) {
            bool correto = true;
            string expressao = @"^[A-Z0-9][A-Za-z0-9 ]{0,19}$";
            if (String.IsNullOrEmpty(xValue))
                return false;
            if (!Regex.Match(xValue, expressao).Success) {
                return false;
            }
            return correto;
        }
        public bool ConsistirNome(string xValue) {
            bool correto = true;
            string expressao = @"^[A-Z][A-Za-z0-9áéíóúÁÉÍÓÚçâõÃÕêÊ,. ]{0,255}$";
            if (String.IsNullOrEmpty(xValue))
                return false;
            if (!Regex.Match(xValue, expressao).Success) {
                return false;
            }
            return correto;
        }
        public bool ConsistirTelefone(string xValue) {
            bool correto = true;
            string expressao = @"^[0-9()][0-9(). ]{7,59}$";
            if (String.IsNullOrEmpty(xValue))
                return false;
            if (!Regex.Match(xValue, expressao).Success) {
                return false;
            }
            return correto;
        }
        public bool ConsistirContato(string xValue) {
            bool correto = true;
            string expressao = @"^[A-Z][A-Za-z0-9áéíóúÁÉÍÓÚçâõÃÕêÊ,. ]{0,89}$";
            if (String.IsNullOrEmpty(xValue))
                return false;
            if (!Regex.Match(xValue, expressao).Success) {
                return false;
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
        public bool ConsistirRazao(string xValue) {
            bool correto = true;
            string expressao = @"^[A-Z][A-Za-z0-1áéíóúÁÉÍÓÚçâõÃÕêÊ,. ]{0,255}$";
            if (String.IsNullOrEmpty(xValue))
                return false;
            if (!Regex.Match(xValue, expressao).Success) {
                return false;
            }
            return correto;
        }
        public bool ConsistirDias(int xValue) {
            bool correto = true;
            if ((xValue <  0)
            || (xValue  >  365)) {
                correto = false;
            }
            return correto;
        }
        public bool ConsistirQualidade(int xValue) {
            bool correto = true;
            if ((xValue < 0)
            || (xValue > 365)) {
                correto = false;
            }
            return correto;
        }
        public bool ConsistirCentralizadoraId(string x) {
            bool correto = true;
            if (x != null) {
                var db = new Contexto();
                if (db.Clientes.FirstOrDefault(p => p.Id == x) == null)
                    correto = false;
            }
            return correto;
        }
        public bool ConsistirCentralizadora(Cliente x) {
            bool correto = true;
            if (x != null) {
                var db = new Contexto();
                if  (db.Clientes.FirstOrDefault(p => p.Id == x.Id) == null)
                    correto = false;
            }
            return correto;
        }

    }
}
