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

    public class Diario  {

        private int id;
        private string responsavelId;
        private Responsavel responsavel;
        private DateTime edicao;
        private string funcao;
        private string operacao;
        private string registro;
        private string coluna;
        private string chave;

        string[] tabOperacoes = { "Incluir", "Alterar", "Excluir", "Entrar", "Sair", "Erro", "Emitir" };


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Identificação deve ser informada.")]
        public int Id {
            get { return id; }
            set { if (ConsistirId(value)) id = value; else throw new Exception("Erro Id."); }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Responsável deve ser informado.")]
        [StringLength(20, ErrorMessage = "Id do responsável de com tamanho maior que o permitido (20).")]
        public String ResponsavelId {
            get { return responsavelId; }
            set { if (ConsistirResponsavelId(value)) responsavelId = value; else throw new Exception("Erro Responsável Id."); }
        }

        public Responsavel Responsavel {
            get { return responsavel; }
            set { if (ConsistirResponsavel(value)) responsavel = value; else throw new Exception("Erro Responsável."); }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Edição deve ser informado.")]
        public DateTime Edicao {
            get { return edicao; }
            set { if (ConsistirEdicao(value)) edicao = value; else throw new Exception("Erro Edição."); }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Função deve ser informado.")]
        [StringLength(128, ErrorMessage = "Função com tamanho maior que o permitido (128).")]
        [RegularExpression(@"^[A-Z0-9a-z][A-Za-z0-9áéíóúÁÉÍÓÚçâõÃÕêÊ,. ]{0,127}$", ErrorMessage = "Função com formação inválida.")]
        public string Funcao {
            get { return funcao; }
            set { if (ConsistirFuncao(value)) funcao = value; else { throw new Exception("Erro função."); } }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Operação deve ser informado.")]
        [StringLength(128, ErrorMessage = "Operação de {0} com tamanho maior que o permitido (128).")]
        [RegularExpression(@"Entrar|Sair|Incluir|Alterar|Excluir|Listar|Erro", ErrorMessage = "Operação de {0} com formação inválida.")]
        public string Operacao {
            get { return operacao; }
            set { if (ConsistirOperacao(value)) operacao = value; else { throw new Exception("Erro Operação."); } }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Registro deve ser informado.")]
        [StringLength(128, ErrorMessage = "Registro com tamanho maior que o permitido (128).")]
        [RegularExpression(@"^[A-Z0-9a-z][A-Za-z0-9áéíóúÁÉÍÓÚçâõÃÕêÊ,. ]{0,127}$", ErrorMessage = "Registro com formação inválida.")]
        public string Registro {
            get { return registro; }
            set { if (ConsistirRegistro(value)) registro = value; else { throw new Exception("Erro registro."); } }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Coluna deve ser informado.")]
        [StringLength(128, ErrorMessage = "Coluna de {0} com tamanho maior que o permitido (128).")]
        [RegularExpression(@"^[A-Z0-9a-z][A-Za-z0-9áéíóúÁÉÍÓÚçâõÃÕêÊ,. ]{0,127}$", ErrorMessage = "Coluna de {0} com formação inválida.")]
        public string Coluna {
            get { return coluna; }
            set { if (ConsistirColuna(value)) coluna = value; else { throw new Exception("Erro coluna de {0}."); } }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Chave de {0} deve ser informado.")]
        [StringLength(128, ErrorMessage = "Chave {0} com tamanho maior que o permitido (128).")]
        [RegularExpression(@"^[A-Z0-9a-z][A-Za-z0-9áéíóúÁÉÍÓÚçâõÃÕêÊ,. ]{0,127}$", ErrorMessage = "Chave de {0} com formação inválida.")]
        public string Chave {
            get { return chave; }
            set { if (ConsistirChave(value)) chave = value; else { throw new Exception("Erro chave de {0}."); } }
        }


        public bool ConsistirId(int x) {
            bool correto = true;
            return correto;
        }
        public bool ConsistirResponsavelId(string x) {
            bool correto = true;
            var db = new Contexto();
            if  (db.Responsaveis.FirstOrDefault(p => p.Id == x) == null)
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
        public bool ConsistirEdicao(DateTime x) {
            bool correto = true;
            return correto;
        }
        public bool ConsistirFuncao(string x) {
            bool correto = true;
            string expressao = @".{0,128}$";
            if (String.IsNullOrEmpty(x))
                return false;
            if (!Regex.Match(x, expressao).Success) {
                return false;
            }
            return correto;
        }
        public bool ConsistirOperacao(string xValue) {
            bool correto = false;
            for (int ix = 0; ix < tabOperacoes.Length; ix++) {
                if (xValue.Trim().Equals(tabOperacoes[ix])) {
                    correto = true;
                }
            }
            return correto;
        }
        public bool ConsistirRegistro(string xValue) {
            bool correto = true;
            string expressao = @".{0,128}$";
            if (String.IsNullOrEmpty(xValue))
                return false;
            if (!Regex.Match(xValue, expressao).Success) {
                return false;
            }
            return correto;
        }
        public bool ConsistirColuna(string xValue) {
            bool correto = true;
            string expressao = @".{0,128}$";
            if (String.IsNullOrEmpty(xValue))
                return false;
            if (!Regex.Match(xValue, expressao).Success) {
                return false;
            }
            return correto;
        }
        public bool ConsistirChave(string xValue) {
            bool correto = true;
            string expressao = @".{0,128}$";
            if (String.IsNullOrEmpty(xValue))
                return false;
            if (!Regex.Match(xValue, expressao).Success) {
                return false;
            }
            return correto;
        }
    }
}
