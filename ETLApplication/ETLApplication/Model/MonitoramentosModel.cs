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
using System.Windows.Forms;

namespace ETLApplication.Model
{
    public class MonitoramentosModel:GenericModel
    {
        [Key]
        [Required]
        public int id_arquivo { get; set; }
        [Required]
        public int id_servico { get; set; }
        public string status { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public DateTime data_criacao { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public DateTime data_atualizacao { get; set; }
        public MonitoramentosModel()
        {
            NomeTabela = "tb_monitoramentos";
        }

        public void SetDados(SqlDataReader dr)
        {
            try
            {
                id_servico= Convert.ToInt32(dr["id_servico"].ToString());
                id_arquivo= Convert.ToInt32(dr["id_arquivo"].ToString());
                status = dr["Status"].ToString();
                data_criacao = Convert.ToDateTime(dr["Data_criacao"].ToString());
                data_atualizacao = Convert.ToDateTime(dr["Data_atualizacao"].ToString());
            }
            finally
            {
                dr.Close();
            }

        }
        public List<MonitoramentosModel> GetListaPermissoes(SqlDataReader dr)
        {
            List<MonitoramentosModel> Monitoramentos = new List<MonitoramentosModel>();
            try
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Monitoramentos.Add
                        (
                            new MonitoramentosModel()
                            {
                                id_servico = Convert.ToInt32(dr["id_servico"].ToString()),
                                id_arquivo = Convert.ToInt32(dr["id_arquivo"].ToString()),
                                status = dr["Status"].ToString(),
                                data_criacao = Convert.ToDateTime(dr["Data_criacao"].ToString()),
                                data_atualizacao = Convert.ToDateTime(dr["Data_atualizacao"].ToString())
                            }
                        );
                    }
                }
            }
            finally
            {
                dr.Close();
            }
            return Monitoramentos;
        }

        public override void SetDados(DataGridView dgv)
        {
            if (dgv.CurrentRow != null)
            {
                if (dgv.CurrentRow.Cells["id_servico"].Value.ToString() != "")
                {
                    id_servico = Convert.ToInt32(dgv.CurrentRow.Cells["id_servico"].Value.ToString());
                    id_arquivo = Convert.ToInt32(dgv.CurrentRow.Cells["id_arquivo"].Value.ToString());
                    status = dgv.CurrentRow.Cells["status"].Value.ToString();
                    data_criacao = Convert.ToDateTime(dgv.CurrentRow.Cells["data_criacao"].Value.ToString());
                    data_atualizacao = Convert.ToDateTime(dgv.CurrentRow.Cells["data_atualizacao"].Value.ToString());
                }
            }
        }
        public override void Clear()
        {
            id_servico = 0;
            id_arquivo = 0;
            status = "";
            data_criacao = DateTime.Now;
            data_atualizacao = DateTime.Now;

        }

    }

}
