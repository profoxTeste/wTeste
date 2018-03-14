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

    public class Parametro   {

        private string id;
        private string nome;
        private string descricao;
        private string valor;
        private DateTime edicao;
        private Byte[] rowVersion;

        public virtual ICollection<ParametroResponsavel> ParametrosResponsaveis { get; set; }

        [Key]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Id de {0} deve ser informado.")]
        [StringLength(12, ErrorMessage = "Id de {0} com tamanho maior que o permitido (12).")]
        [RegularExpression(@"^[A-Z0-9][A-Za-z0-9 ]{0,19}$", ErrorMessage = "Id de {0} com formação inválida.")]
        public string Id {
            get { return id; }
            set { if (ConsistirId(value)) id = value; else { throw new Exception("Erro."); } }
        }

        [Column("Nome", Order = 1)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Nome de {0} deve ser informado.")]
        [StringLength(128, ErrorMessage = "Nome de {0} com tamanho maior que o permitido (128).")]
        [RegularExpression(@"^[A-Z][A-Za-z0-9áéíóúÁÉÍÓÚçãõÃÕêÊâÂ,. ]{0,127}$", ErrorMessage = "Nome de {0} com formação inválida.")]
        public string Nome {
            get { return nome; }
            set { if (ConsistirNome(value)) nome = value; else { throw new Exception("Erro Nome."); } }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Descrição de {0} deve ser informado.")]
        [StringLength(5000, ErrorMessage = "Descrição de {0} com tamanho maior que o permitido (5000).")]
        [RegularExpression(@"^[A-Z][A-Za-z0-9áéíóúÁÉÍÓÚçãõÃÕâÂêÊâÂ,. ]{0,4999}$", ErrorMessage = "Descrição de {0} com formação inválida.")]
        public string Descricao {
            get { return descricao; }
            set { if (ConsistirDescricao(value)) descricao = value; else { throw new Exception("Erro Descrição."); } }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Valor de {0} deve ser informado.")]
        [StringLength(256, ErrorMessage = "Valor de {0} com tamanho maior que o permitido (256).")]
        [RegularExpression(@"^[A-Za-z0-9][A-Za-z0-9áéíóúÁÉÍÓÚçãõÃÕêÊâÂ,. ]{0,255}$", ErrorMessage = "Valor de {0} com formação inválida.")]
        public string Valor {
            get { return valor; }
            set { if (ConsistirValor(value)) valor = value; else { throw new Exception("Erro Valor."); } }
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



        
        public bool ConsistirId(string x) {
            bool correto = true;
            return correto;
        }
        public bool ConsistirNome(string xValue) {
            bool correto = true;
            string expressao = @"^[A-Z][A-Za-z0-9áéíóúàÁÉÍÓÚÇçãõÃÕêÊâÂ,. ]{0,127}$";
            if (String.IsNullOrEmpty(xValue))
                return false;
            if (!Regex.Match(xValue, expressao).Success) {
                return false;
            }
            return correto;
        }
        public bool ConsistirDescricao(string xValue) {
            bool correto = true;
            string expressao = @"^[A-Z][A-Za-z0-9áéíóúàÁÉÍÓÚÇçãõÃÕêÊâÂ,. ]{0,4999}$";
            if (String.IsNullOrEmpty(xValue))
                return false;
            if (!Regex.Match(xValue, expressao).Success) {
                return false;
            }
            return correto;
        }
        public bool ConsistirValor(string x) {
            bool correto = true;
            string expressao = @"^.{0,127}$";
            if (String.IsNullOrEmpty(x))
                return false;
            if (!Regex.Match(x, expressao).Success) {
                return false;
            }
            return correto;
        }

    }
}
