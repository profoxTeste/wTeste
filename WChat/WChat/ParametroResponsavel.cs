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

    public class ParametroResponsavel     {

        private int id;
        private string parametroId;
        private Parametro parametro;
        private string responsavelId;
        private Responsavel responsavel;
        private string valor;
        private DateTime edicao;
        private Byte[] rowVersion;


        [Key]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Id deve ser informado.")]
        public int Id {
            get { return id; }
            set { if (ConsistirId(value)) id = value;  else  throw new Exception("Erro Id."); }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Id do Parâmetro deve ser informado.")]
        [StringLength(12, ErrorMessage = "Id do parâmetro com tamanho maior que o permitido (12).")]
        [RegularExpression(@"^[A-Z0-9][A-Za-z0-9 ]{0,19}$", ErrorMessage = "Id do parâmetro com formação inválida.")]
        public string ParametroId {
            get { return parametroId; }
            set { if (ConsistirParametroId(value)) parametroId = value;  else  throw new Exception("Erro Parâmetro Id."); }
        }

        public Parametro Parametro {
            get { return parametro; }
            set { if (ConsistirParametro(value)) parametro = value;  else  throw new Exception("Erro Parâmetro."); }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Responsável deve ser informado.")]
        [StringLength(20, ErrorMessage = "Id do responsável com tamanho maior que o permitido (20).")]
        public String ResponsavelId {
            get { return responsavelId; }
            set { if (ConsistirResponsavelId(value)) responsavelId = value;  else  throw new Exception("Erro ResponsávelId."); }
        }

        public Responsavel Responsavel {
            get { return responsavel; }
            set { if (ConsistirResponsavel(value)) responsavel = value;  else  throw new Exception("Erro Responsável."); }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Valor de {0} deve ser informado.")]
        [StringLength(256, ErrorMessage = "Valor de {0} com tamanho maior que o permitido (256).")]
        [RegularExpression(@"^[A-Za-z0-9][A-Za-z0-9áéíóúÁÉÍÓÚçâõÃÕêÊ,. ]{0,255}$", ErrorMessage = "Valor de {0} com formação inválida.")]
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



        
        public bool ConsistirId(int x) {
            bool correto = true;
            return correto;
        }
        public bool ConsistirParametroId(string x) {
            bool correto = true;
            var db = new Contexto();
            if (db.Parametros.FirstOrDefault(p => p.Id == x) == null)
                correto = false;
            return correto;
        }
        public bool ConsistirParametro(Parametro x) {
            bool correto = true;
            var db = new Contexto();
            if (db.Parametros.FirstOrDefault(p => p.Id == x.Id) == null)
                correto = false;
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
