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

    public class Pendencia    {

        private int id;
        private string clienteId;
        private Cliente cliente;
        private string contato;
        private DateTime dataAbertura;
        private DateTime dataUltimaAtualizacao;
        private string responsavelAberturaId;
        private Responsavel responsavelAbertura;
        private string responsavelAtualId;
        private Responsavel responsavelAtual;
        private string status;
        private string motivo;
        private string assuntoId;
        private Assunto assunto;
        private string detalhamentoAbertura;
        private string detalhamentoAtual;
        private string detalhamentoFinal;
        private DateTime edicao;
        private Byte[] rowVersion;

        private string[] tabStatus  = { "Aberto", "Fechado" };
        private string[] tabMotivos = { "Aguardando Autorização", "Aguardando Cliente",
                                        "Autorizado", "Em Execução", "Pronto", "Em Teste"};


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Identificação da pendência deve ser informada.")]
        public int Id {
            get { return id; }
            set { if (ConsistirId(value)) id = value; else throw new Exception("Erro Id."); }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Id do Cliente {0} deve ser informado.")]
        [StringLength(20, ErrorMessage = "Id do cliente {0} com tamanho maior que o permitido (20).")]
        public String ClienteId {
            get { return clienteId; }
            set { if (ConsistirClienteId(value)) clienteId = value; else throw new Exception("Erro Cliente Id."); }
        }

        public Cliente Cliente {
            get { return cliente; }
            set { if (ConsistirCliente(value)) cliente = value; else throw new Exception("Erro Cliente."); }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Contato de {0} deve ser infoemado.")]
        [StringLength(20, ErrorMessage = "Contato de {0} com tamanho maior que o permitido (20).")]
        [RegularExpression(@"^[A-Z][A-Za-z0-9áéíóúÁÉÍÓÚÇçâõÃÕêÊ,. ]{0,19}$", ErrorMessage = "Detalhamento da agenda com formação inválida.")]
        public string Contato {
            get { return contato; }
            set { if (ConsistirContato(value)) contato = value; else { throw new Exception("Erro Contato."); } }
        }

        [Column("DataAbertura")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Data de abertura de {0} deve ser informada.")]
        public DateTime DataAbertura {
            get { return dataAbertura; }
            set { if (ConsistirData(value)) dataAbertura = value; else throw new Exception("Erro Data de Abertura."); }
        }

        [Column("DataUltimaAtualizacao")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Data da última atualização de {0} deve ser informada.")]
        public DateTime DataUltimaAtualizacao {
            get { return dataUltimaAtualizacao; }
            set { if (ConsistirData(value)) dataUltimaAtualizacao = value; else throw new Exception("Erro Data Última Atualização."); }
        }

        [Column("ResponsavelAberturaId")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Responsável pela abertura de {0} deve ser informado.")]
        [StringLength(20, ErrorMessage = "Id do responsável pela abertura de {0} com tamanho maior que o permitido (20).")]
        public String ResponsavelAberturaId {
            get { return responsavelAberturaId; }
            set { if (ConsistirResponsavelId(value)) responsavelAberturaId = value; else throw new Exception("Erro Responsável Abertura Id."); }
        }

        public Responsavel ResponsavelAbertura {
            get { return responsavelAbertura; }
            set { if (ConsistirResponsavel(value)) responsavelAbertura = value; else throw new Exception("Erro Responsável Abertura"); }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Responsável atual de {0} deve ser informado.")]
        [StringLength(20, ErrorMessage = "Id do responsável atual de {0} com tamanho maior que o permitido (20).")]
        public String ResponsavelAtualId {
            get { return responsavelAberturaId; }
            set { if (ConsistirResponsavelId(value)) responsavelAtualId = value; else throw new Exception("Erro Responsável Atual Id."); }
        }

        public Responsavel ResponsavelAtual {
            get { return responsavelAtual; }
            set { if (ConsistirResponsavel(value)) responsavelAtual = value; else throw new Exception("Erro Responsável Atual."); }
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
        [RegularExpression(@"Aguardando Autorização|Aguardando Cliente|Autorizado|Em Execução|Não Autorizado|Pronto|Em Teste", ErrorMessage = "Motivo de {0} com formação inválida.")]
        public string Motivo {
            get { return motivo; }
            set { if (ConsistirMotivo(value)) motivo = value; else { throw new Exception("Erro Motivo."); } }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Id do assunto de {0} deve ser informado.")]
        [StringLength(20, ErrorMessage = "Id do assunto de {0} com tamanho maior que o permitido (20).")]
        public String AssuntoId {
            get { return assuntoId; }
            set { if (ConsistirAssuntoId(value)) assuntoId = value; else throw new Exception("Erro Assunto Id."); }
        }

        public Assunto Assunto {
            get { return assunto; }
            set { if (ConsistirAssunto(value)) assunto = value; else throw new Exception("Erro Assunto."); }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Detalhamento de abertura de {0} deve ser informado.")]
        [StringLength(5000, ErrorMessage = "Detalhamento de abertura de {0} com tamanho maior que o permitido (5000).")]
        [RegularExpression(@"^[A-Z][A-Za-z0-9áéíóúÁÉÍÓÚçâõÃÕêÊ,. ]{0,4999}$", ErrorMessage = "Detalhamento de abertura de {0} com formação inválida.")]
        public string DetalhamentoAbertura {
            get { return detalhamentoAbertura; }
            set { if (ConsistirDetalhamento(value)) detalhamentoAbertura = value; else { throw new Exception("Erro detalhamento de abertura de {0}."); } }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Detalhamento atual de {0} deve ser informado.")]
        [StringLength(5000, ErrorMessage = "Detalhamento atual de {0} com tamanho maior que o permitido (5000).")]
        [RegularExpression(@"^[A-Z][A-Za-z0-9áéíóúÁÉÍÓÚçâõÃÕêÊ,. ]{0,4999}$", ErrorMessage = "Detalhamento atual de {0} com formação inválida.")]
        public string DetalhamentoAtual {
            get { return detalhamentoAtual; }
            set { if (ConsistirDetalhamento(value)) detalhamentoAtual = value; else { throw new Exception("Erro detalhamento atual de {0}."); } }
        }

        [Column("DetalhamentoFinal")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Detalhamento final de {0} deve ser informado.")]
        [StringLength(5000, ErrorMessage = "Detalhamento final de {0} com tamanho maior que o permitido (5000).")]
        [RegularExpression(@"^[A-Z][A-Za-z0-9áéíóúÁÉÍÓÚçâõÃÕêÊ,. ]{0,4999}$", ErrorMessage = "Detalhamento final de {0} com formação inválida.")]
        public string DetalhamentoFinal {
            get { return detalhamentoFinal; }
            set { if (ConsistirDetalhamento(value)) detalhamentoFinal = value; else { throw new Exception("Erro detalhamento final de {0}."); } }
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


        public virtual ICollection<PendenciaHistorico> PendenciaHistorico { get; set; }



        
        public bool ConsistirId(int x) {
            bool correto = true;
            if (x < 1)
                return false;
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
            if (db.Clientes.FirstOrDefault(p => p.Id == x.Id) == null)
                correto = false;
            return correto;
        }
        public bool ConsistirContato(string x) {
            bool correto = true;
            string expressao = @"^[A-Z][A-Za-z0-9áéíóúÁÉÍÓÚçâõÃÕêÊ,. ]{0,19}$";
            if (String.IsNullOrEmpty(x))
                return false;
            if (!Regex.Match(x, expressao).Success) {
                return false;
            }
            return correto;
        }
        public bool ConsistirData(DateTime xValue) {
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
