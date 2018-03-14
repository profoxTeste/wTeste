using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;

namespace WChat
{

    public class Assunto {

        private string id;
        private string nome;
        private DateTime edicao;
        private Byte[] rowVersion;

        [Key]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Identificação do assunto deve ser informado.")]
        [StringLength(20, ErrorMessage = "Identificação do assunto com tamanho maior que o permitido (20).")]
        [RegularExpression(@"^[A-Z0-9a-z][A-Za-z0-9 ]{0,19}$", ErrorMessage = "Identificação do assunto com formação errada.")]
        public string Id {
            get { return id; }
            set {
                if (ConsistirId(value)) {
                    id = value;
                }
                else {
                    throw new Exception("Erro.");
                }
            }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Nome do assunto deve ser informado.")]
        [StringLength(256, ErrorMessage = "Nome do assunto com tamanho maior que o permitido (256).")]
        [RegularExpression(@"^[A-Z][A-Za-z0-9áéíóúÁÉÍÓÚçâõÃÕêÊ,. ]{0,255}$", ErrorMessage = "Nome do assunto com formação errada.")]
        public string Nome {
            get { return nome; }
            set {
                if (ConsistirNome(value)) {
                    nome = value;
                }   else { 
                    throw new Exception("Erro na na definição da propriedade Nome."); 
                }
            }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Edição deve ser informado.")]
        public DateTime Edicao { get;  set; }

        [Timestamp]
        public Byte[] RowVersion { get; set; }


        public virtual ICollection<Atendimento> Atendimentos { get; set; }
        public virtual ICollection<Pendencia> Pendencias { get; set; }



        public bool Consistir(string id, string nome)  {
            bool correto = true;
            if (correto) { correto = ConsistirId(id); }
            if (correto) { correto = ConsistirNome(nome); }
            return correto;
        }
        public bool ConsistirId(string xValue)  {
            bool correto = true;
            string expressao = @"^[A-Z0-9a-z][A-Za-z0-9 ]{0,19}$";
            if (String.IsNullOrEmpty(xValue))
                return false;
            if (!Regex.Match(xValue, expressao).Success) {
                return false;
            }
            return correto;
        }
        public bool ConsistirNome(string xValue)  {
            bool correto = true;
            string expressao = @"^[A-Z][A-Za-z0-9áéíóúÁÉÍÓÚçâõÃÕêÊ,. ]{0,255}$";
            if (String.IsNullOrEmpty(xValue))
                return false;
            if (!Regex.Match(xValue, expressao).Success) {
                return false;
            }
            return correto;
        }


    }
}
