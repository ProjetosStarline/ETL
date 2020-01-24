/*
	==============================================
	Criado por : Carlos R. S. Oliveira
	Criado em  : 20/05/2019
	==============================================
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETLApplication.Model
{
    public class ExpImpTabelasModel
    {
        public List<GruposModel> Grupos { get; set; }
        public List<CategoriasModel> Categorias { get; set; }
        public List<PacotesModel> Pacotes { get; set; }
        public List<ServicosModel> Servicos { get; set; }
        public List<MonitoramentosModel> Monitoramentos { get; set; }
        public List<ArquivosModel> Arquivos { get; set; }
        public List<MapeamentosModel> Mapeamentos { get; set; }
        public List<PerfisModel> Perfis { get; set; }
        public List<UsuariosModel> Usuarios { get; set; }
        public List<ObjetosModel> Objetos { get; set; }
        public List<PermissoesModel> Permissoes { get; set; }
        public List<ParametrosModel> Parametros { get; set; }

        public ExpImpTabelasModel()
        {
            Grupos = new List<GruposModel>();
            Categorias = new List<CategoriasModel>();
            Pacotes = new List<PacotesModel>();
            Servicos = new List<ServicosModel>();
            Monitoramentos = new List<MonitoramentosModel>();
            Arquivos = new List<ArquivosModel>();
            Mapeamentos = new List<MapeamentosModel>();
            Perfis = new List<PerfisModel>();
            Usuarios = new List<UsuariosModel>();
            Objetos = new List<ObjetosModel>();
            Permissoes = new List<PermissoesModel>();
            Parametros = new List<ParametrosModel>();
        }

    }
}
