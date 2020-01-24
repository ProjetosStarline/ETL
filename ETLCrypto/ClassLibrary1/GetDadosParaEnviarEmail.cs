/*
	==============================================
	Criado por : Carlos R. S. Oliveira
	Criado em  : 20/05/2019
	==============================================
*/
using EtlConexao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facade
{
    class GetDadosParaEnviarEmail
    {
        public enum TipoDadosEmpresa {Site,Logo};
        Conexao conexao;
        public GetDadosParaEnviarEmail()
        {
            conexao = new Conexao();
        }
        public string GetDadosEmpresa(int pIdServico,TipoDadosEmpresa tpDadosEmpresa)
        {
            string Result = "";
            string select = "";
            select += "select distinct e.Site_Empresa,e.Logo_Empresa ";
            select += " from tb_grupos e, ";
            select += "     tb_monitoramentos m, ";
            select += "	    tb_categorias f, ";
            select += "	    tb_arquivos a, ";
            select += "	    tb_pacotes p";
            select += " where a.id_arquivo=m.id_arquivo";
            select += "   and p.id_pacote=a.id_pacote";
            select += "   and f.id_categoria=p.id_categoria";
            select += "   and e.id_grupo=f.id_grupo";
            select += $"  and m.id_servico={pIdServico.ToString()}";
            conexao.AbrirConexao(conexao.Conn);
            SqlCommand cmd = new SqlCommand(select, conexao.Conn);
            cmd.CommandTimeout = 0;
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows && dr.Read())
            {
                if (tpDadosEmpresa == TipoDadosEmpresa.Site)
                {
                    Result = dr["Site_Empresa"].ToString();
                }
                else
                {
                    Result = dr["Logo_Empresa"].ToString();
                }
            }
            dr.Close();
            return Result;
        }
        public string ObtemSelect()
        {
            string select = "";
            select += "select pacote 'Pacote', id_arquivo 'ID', arquivo 'Arquivo', tabela_destino 'Tabela Destino', inicio 'Início', termino 'Término', tempo 'Tempo', status 'Status'";
            select += " from (";
            select += "    select Pacote, id_arquivo, Arquivo, Tabela_Destino, data as inicio, ultima_data as termino, convert(time, ultima_data - data) 'Tempo',";
            select += "           case when move_mensagem like '%True%' then 'SUCESSO' else 'ERRO' end as status,";
            select += "           dense_rank() over(partition by id_arquivo order by data desc) as rank";
            select += "    from (select p.nm_pacote 'Pacote', a.id_arquivo, a.nm_arquivo 'Arquivo', a.tb_destino 'Tabela_Destino', l.data, l.mensagem,";
            select += "               lead(l.data, 1) over(partition by l.id_arquivo order by data) as ultima_data,";
            select += "               lead(l.mensagem, 1) over(partition by l.id_arquivo order by data) as ultima_mensagem,";
            select += "               lead(l.data, 2) over(partition by l.id_arquivo order by data) as move_data,";
            select += "               lead(l.mensagem, 2) over(partition by l.id_arquivo order by data) as move_mensagem";
            select += "          from tb_logs l, tb_arquivos a, tb_pacotes p";
            select += "         where l.id_arquivo = a.id_arquivo";
            select += "           and a.id_pacote = p.id_pacote";
            select += "           and(mensagem like 'INICIO DO ARQUIVO%'";
            select += "            or mensagem like 'TÉRMINO DO ARQUIVO%'";
            select += "            or mensagem like 'Move Para Processados(%)')";
            select += "         ) as src";
            select += "   where (mensagem like 'INICIO DO ARQUIVO%'";
            select += "          and ultima_mensagem like 'TÉRMINO DO ARQUIVO%'";
            select += "          and move_mensagem like 'Move Para Processados(%)')";
            select += "      or(mensagem like 'INICIO DO ARQUIVO%' and ultima_mensagem is null)";
            select += ") as consulta where rank = 1";
            select += " and status = 'ERRO'";
            select += " order by Pacote";

            return select;

        }

        public DataTable GetDados()
        {
            DataTable dt = new DataTable();
            string select = ObtemSelect();
            conexao.AbrirConexao(conexao.Conn);
            SqlCommand cmd = new SqlCommand(select, conexao.Conn);
            cmd.CommandTimeout = 0;
            dt.Load(cmd.ExecuteReader());
            return dt;
        }

        public string GeraFileHtml(int pIDServico)
        {
            string Result = "";
            string Html = "";
            string Head = "";
            string Body = "";

            Html += "<!DOCTYPE html PUBLIC ' -//W3C//DTD XHTML 1.0 Strict//EN'\n";
            Html += "'http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd'>\n";
            Html += "<html xmlns='http://www.w3.org/1999/xhtml'>\n";

            Head += "   <head>\n";
            Head += "      <style id='Tabela'>\n";
            Head += "      <!--table \n";
            Head += "      	{mso-displayed-decimal-separator:'\\,'; \n";
            Head += "      	mso-displayed-thousand-separator:'\\.';} \n";
            Head += "      .tabela{ \n";
            Head += "      	border-collapse: collapse; \n";
            Head += "      	table-layout:fixed; \n";
            Head += "      	width:725pt; \n";
            Head += "       } \n";
            Head += "      ";
            Head += "      .CabecalhoTable {	                     \n";
            Head += "      	padding-top:1px;                         \n";
            Head += "      	padding-right:1px;                       \n";
            Head += "      	padding-left:1px;                        \n";
            Head += "      	mso-ignore:padding;                        ";
            Head += "      	color:black;                             \n";
            Head += "      	font-size:11.0pt;                        \n";
            Head += "      	font-weight:700;                         \n";
            Head += "      	font-style:normal;                       \n";
            Head += "      	text-decoration:none;                    \n";
            Head += "      	font-family:Calibri, sans-serif;         \n";
            Head += "      	mso-font-charset:0;                      \n";
            Head += "      	mso-number-format:General;               \n";
            Head += "      	text-align:general;                      \n";
            Head += "      	vertical-align:bottom;                   \n";
            Head += "      	background:#D9D9D9;                      \n";
            Head += "      	mso-pattern:black none;                  \n";
            Head += "      	white-space:nowrap;						 \n";
            Head += "      	border-top:1.0pt solid windowtext;       \n";
            Head += "      	border-right:.5pt solid windowtext;      \n";
            Head += "      	border-bottom:1.0pt solid windowtext;    \n";
            Head += "      	border-left:1.0pt solid windowtext;      \n";
            Head += "      }                     					 \n";
            Head += "                                                  ";
            Head += "      .CorCabecalho {	                         \n";
            Head += "      	padding-top:1px;                         \n";
            Head += "      	padding-right:1px;                       \n";
            Head += "      	padding-left:1px;                        \n";
            Head += "      	mso-ignore:padding;                      \n";
            Head += "      	color:black;                             \n";
            Head += "      	font-size:11.0pt;                        \n";
            Head += "      	font-weight:700;                         \n";
            Head += "      	font-style:normal;                       \n";
            Head += "      	text-decoration:none;                    \n";
            Head += "      	font-family:Calibri, sans-serif;         \n";
            Head += "      	mso-font-charset:0;                      \n";
            Head += "      	mso-number-format:General;               \n";
            Head += "      	text-align:general;                      \n";
            Head += "      	vertical-align:bottom;                   \n";
            Head += "      	background:#D9D9D9;                      \n";
            Head += "      	mso-pattern:black none;                  \n";
            Head += "      	white-space:nowrap;						 \n";
            Head += "      	border-top:1.0pt solid windowtext;       \n";
            Head += "      	border-right:.5pt solid windowtext;      \n";
            Head += "      	border-bottom:1.0pt solid windowtext;    \n";
            Head += "      	border-left:.5pt solid windowtext;       \n";
            Head += "      }                         				 \n";
            Head += "                                                \n";
            Head += "      .Registro {                       \n";
            Head += "      	padding-top:1px;                 \n";
            Head += "      	padding-right:1px;               \n";
            Head += "      	padding-left:1px;                \n";
            Head += "      	mso-ignore:padding;              \n";
            Head += "      	color:black;                     \n";
            Head += "      	font-size:11.0pt;                \n";
            Head += "      	font-weight:400;                 \n";
            Head += "      	font-style:normal;               \n";
            Head += "      	text-decoration:none;            \n";
            Head += "      	font-family:Calibri, sans-serif; \n";
            Head += "      	mso-font-charset:0;              \n";
            Head += "      	mso-number-format:General;       \n";
            Head += "      	text-align:general;              \n";
            Head += "      	vertical-align:bottom;           \n";
            Head += "      	mso-background-source:auto;		 \n";
            Head += "      	mso-pattern:auto;				 \n";
            Head += "      	white-space:nowrap;				 \n";
            Head += "      	border:.5pt solid windowtext;    \n";
            Head += "      }             					 \n";
            Head += "                                        \n";
            Head += "      -->                               \n";
            Head += "      </style>                          \n";
            Head += "   </head>                              \n";

            DataTable dt = GetDados();

            //Cabecalho da Tabela
            string HeadTable = "                <tr class=CabecalhoTable> \n";
            for (int col = 0; col < dt.Columns.Count; col++)
            {
                HeadTable += $"                 <td class=CabecalhoTable >{dt.Columns[col].ColumnName}</td> \n";
            }
            HeadTable += "              </tr> \n";

            string RegTable = "";
            //Registros da Tabela
            for (int row = 0; row < dt.Rows.Count; row++)
            {
                RegTable += "             <tr class=Registro> \n";
                for (int col = 0; col < dt.Columns.Count; col++)
                {
                    RegTable += $"                  <td class=Registro >{dt.Rows[row].ItemArray[col]}</td> \n";
                }
                RegTable += "               </tr> \n";
            }

            string SiteEmpresa = "Http://"+GetDadosEmpresa(pIDServico, TipoDadosEmpresa.Site);
            string LogoEmpresa = "Http://"+GetDadosEmpresa(pIDServico, TipoDadosEmpresa.Logo);

            Body += "    <body> \n"; 
            Body += "     <div id='ArquivosComErro' align=center> \n";
            Body += "           <table border=0 cellpadding=0 cellspacing=0 width=965 class=Tabela > \n";
            Body += "               <tr> \n";
            Body += "                   <td> \n";
            Body += "                   <td> \n";
            Body += "                     <table width='100%' align='center' border='0' cellpadding='0' cellspacing='0'> \n";
            Body += "                       <tr> \n";
            Body += "                         <td width='15%' height='50' style='text-align:left'> \n";
            Body += "                           <a href='http://www.starline.inf.br'> <img src='http://www.starline.inf.br/logo/starline.png' height='50' alt='' border='0'> </a> \n";
            Body += "                         </td> \n";
            Body += "                         <td width='70%' height='50' style='text-align:center;font-family:Arial,Verdana,sans-serif;color:#003767;font-size:22px;vertical-align:middle;padding-left:24px;padding-top:12px'> \n";
            Body += $"                               Estatísticas de Processamento do Serviço de Carga ETL (ID {pIDServico.ToString()})</td> \n";
            Body += "                         <td width='15%' height='50' style='text-align:right'> \n";
            Body += $"                           <a href='{SiteEmpresa}'> <img src='{LogoEmpresa}' height='50' alt='' border='0' > </a> \n";
            Body += "                         </td> \n";
            Body += "                       </tr> \n";
            Body += "                     </table> \n";
            Body += "                     <table width='100%' align='center' border='0' cellpadding='0' cellspacing='0'> \n";
            Body += "                       <tr>  <td width='100%' height='4' bgcolor='#99b3ff' style='font-size:0px'>&nbsp;</td>  </tr> \n";
            Body += "                     </table> \n";
            Body += "                     <br> \n";
            Body += "                     <table border=0 cellpadding=0 cellspacing=0 class=Tabela >  \n";
            Body += $"                      {HeadTable}";
            Body += $"                      {RegTable}";
            Body += "                     </table>";
            Body += "                   <br> \n";
            Body += "                   <table width='100%' align='center' border='0' cellpadding='0' cellspacing='0'> \n";
            Body += "                       <tr> \n";
            Body += "                           <td width='85%' bgcolor='#efe9e5' align='left' style='font-family:Arial,Verdana,sans-serif;color:#6c6d6f;font-size:12px;padding-left:20px;padding-top:12px;padding-bottom:12px'><strong>Otimizando os resultados do seu neg&oacute;cio com solu&ccedil;&otilde;es digitais</strong></td> \n";
            Body += $"                           <td width='15%' bgcolor='#efe9e5' align='left' style='font-family:Arial,Verdana,sans-serif;color:#6c6d6f;font-size:12px;padding-left:20px;padding-top:12px;padding-bottom:12px'>&copy; Starline {DateTime.Now.Year.ToString()}</td> \n";
            Body += "                       </tr> \n";
            Body += "                   </table> \n";
            Body += "               </td> \n";
            Body += "           </tr> \n";
            Body += "       </table> \n";
            Body += "       <br> \n";
            Body += "    </div>  \n";
            Body += "   </body> \n";

            Html += Head;
            Html += Body;
            Html += "</html> \n";

            Result = Html;

            return Result;
        }
    }
}
