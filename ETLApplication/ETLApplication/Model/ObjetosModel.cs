/*
	==============================================
	Criado por : Carlos R. S. Oliveira
	Criado em  : 20/05/2019
	==============================================
*/
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETLApplication.Model
{
    public class ObjetosModel:GenericModel
    {
        public ObjetosModel()
        {
            NomeTabela = "tb_objetos";
        }
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]// Esta linha informa que o campo nao pode ser incluso no script de Inclusao ou Alteracao
        public int Id_objeto { get; set; }
        [StringLength(128)]
        public string Nm_objeto { get; set; }
        [StringLength(64)]
        public string Tp_objeto { get; set; }
        [StringLength(16)]
        public string Status { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]// Esta linha informa que o campo nao pode ser incluso no script de Inclusao ou Alteracao
        public DateTime Data_criacao { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]// Esta linha informa que o campo nao pode ser incluso no script de Inclusao ou Alteracao
        public DateTime Data_atualizacao { get; set; }

        public void SetDadosObjeto(SqlDataReader dr)
        {
            try
            {
                if (dr.HasRows && dr.Read())
                {
                    Id_objeto = Convert.ToInt32(dr["Id_objeto"].ToString());
                    Nm_objeto = dr["Nm_objeto"].ToString();
                    Tp_objeto = dr["Tp_objeto"].ToString();
                    Status = dr["Status"].ToString();
                    Data_criacao = Convert.ToDateTime(dr["Data_criacao"].ToString());
                    Data_atualizacao = Convert.ToDateTime(dr["Data_atualizacao"].ToString());
                }
            }
            finally
            {
                dr.Close();
            }
        }

        public List<ObjetosModel> GetListObjetos(SqlDataReader dr)
        {
            List<ObjetosModel> LstObjetos = new List<ObjetosModel>();
            try
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        LstObjetos.Add(
                            new ObjetosModel()
                            {
                                Id_objeto = Convert.ToInt32(dr["Id_objeto"].ToString()),
                                Nm_objeto = dr["Nm_objeto"].ToString(),
                                Tp_objeto = dr["Tp_objeto"].ToString(),
                                Status = dr["Status"].ToString(),
                                Data_criacao = Convert.ToDateTime(dr["Data_criacao"].ToString()),
                                Data_atualizacao = Convert.ToDateTime(dr["Data_atualizacao"].ToString())
                            }
                        );
                    }
                }
            }
            finally
            {
                dr.Close();
            }
            return LstObjetos;
        }
        public override void Clear()
        {
            Id_objeto = 0;
            Nm_objeto = "";
            Tp_objeto = "";
            Status = "";
            Data_criacao = DateTime.Now;
            Data_atualizacao = DateTime.Now;

        }
    }
}
