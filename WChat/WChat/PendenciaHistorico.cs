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

    public class PendenciaHistorico     {

        private int id;
        private int pendenciaId;
        private Pendencia pendencia;
        private DateTime data;
        private string responsavelId;
        private Responsavel responsavel;
        private string status;
        private string motivo;
        private string detalhamento;
        private DateTime edicao;
        private Byte[] rowVersion;

        private string[] tabStatus = { "Aberto", "Fechado" };
        private string[] tabMotivos = { "Aguardando Autorização", "Aguardando Cliente",
                                        "Autorizado", "Em Execução", "Pronto", "Em Teste"};


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Identificação deve ser informada.")]
        public int Id {
            get { return id; }
            set { if (ConsistirId(value)) id = value; else throw new Exception("Erro Id."); }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Id da pendência de {0} deve ser informado.")]
        public int PendenciaId {
            get { return pendenciaId; }
            set { if (ConsistirPendenciaId(value)) pendenciaId = value; else throw new Exception("Erro Pendência Id."); }
        }

        public Pendencia Pendencia {
            get { return pendencia; }
            set { if (ConsistirPendencia(value)) pendencia = value; else throw new Exception("Erro Pendência."); }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Data de {0} deve ser informada.")]
        public DateTime Data {
            get { return data; }
            set { if (ConsistirData(value)) data = value; else throw new Exception("Erro Data."); }
        }


        [Required(AllowEmptyStrings = false, ErrorMessage = "Responsável de {0} deve ser informado.")]
        [StringLength(20, ErrorMessage = "Id do responsável de {0} com tamanho maior que o permitido (20).")]
        public String ResponsavelId {
            get { return responsavelId; }
            set { if (ConsistirResponsavelId(value)) responsavelId = value; else throw new Exception("Erro Responsável Id."); }
        }

        public Responsavel Responsavel {
            get { return responsavel; }
            set { if (ConsistirResponsavel(value)) responsavel = value; else throw new Exception("Erro Responsável."); }
        }


        [Required(AllowEmptyStrings = false, ErrorMessage = "Status de {0} deve ser informado.")]
        [StringLength(10, ErrorMessage = "Status de {0} com tamanho maior que o permitido (10).")]
        [RegularExpression(@"Aberto|Fechado", ErrorMessage = "Status de {0} com formação inválida.")]
        public string Status {
            get { return status; }
            set { if (ConsistirStatus(value)) status = value; else { throw new Exception("Erro Status."); } }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Motivo do status de {0} deve ser informado.")]
        [StringLength(10, ErrorMessage = "Motivo de status de {0} com tamanho maior que o permitido (10).")]
        [RegularExpression(@"Aguardando Autorização|Aguardando Cliente|Autorizado|Em Execução|Não Autorizado|Pronto|Em Teste", ErrorMessage = "Motivo  com formação inválida.")]
        public string Motivo {
            get { return motivo; }
            set { if (ConsistirMotivo(value)) motivo = value; else { throw new Exception("Erro Motivo."); } }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Detalhamento de {0} deve ser informado.")]
        [StringLength(5000, ErrorMessage = "Detalhamento de {0} com tamanho maior que o permitido (5000).")]
        [RegularExpression(@"^[A-Z][A-Za-z0-9áéíóúÁÉÍÓÚçâõÃÕêÊ,. ]{0,4999}$", ErrorMessage = "Detalhamento de {0} com formação inválida.")]
        public string Detalhamento {
            get { return detalhamento; }
            set { if (ConsistirDetalhamento(value)) detalhamento = value; else { throw new Exception("Erro detalhamento."); } }
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
        public bool ConsistirPendenciaId(int x) {
            bool correto = true;
            var db = new Contexto();
            if (db.Pendencias.FirstOrDefault(p => p.Id == x) == null)
                correto = false;
            return correto;
        }
        public bool ConsistirPendencia(Pendencia x) {
            bool correto = true;
            var db = new Contexto();
            if (db.Pendencias.FirstOrDefault(p => p.Id == x.Id) == null)
                correto = false;
            return correto;
        }
        public bool ConsistirData(DateTime x) {
            bool correto = true;
            return correto;
        }
        public bool ConsistirResponsavelId(string x) {
            bool correto = true;
            var db = new Contexto();
            if (db.Responsaveis.FirstOrDefault(p => p.Id == x) == null)
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
