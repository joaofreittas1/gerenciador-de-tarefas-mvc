using System.Data.SqlClient;

namespace AplicacaoWebTarefas.Models
{
    public class Tarefa
    {
        private readonly static string _coon = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=HOMOLOGACAOTESTE;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public int CodigoTarefa { get; set; }

        public string NomeTarefa { get; set; }

        public decimal Custo { get; set; }

        public DateTime DateLimite { get; set; }

        public int OrdemApresentacao { get; set; }

        public Tarefa(int codigotarefa,string nometarefa, decimal custo, DateTime datalimite, int ordemapresentacao)
        {
            CodigoTarefa = codigotarefa;
            NomeTarefa = nometarefa;
            Custo = custo;
            DateLimite = datalimite;
            OrdemApresentacao = ordemapresentacao;
        }

        public Tarefa()
        {  }

        public static List<Tarefa> GetTarefa()
        {
            var listaTarefas = new List<Tarefa>();
            var sql = "SELECT * FROM tb_tarefa order by nr_ordem_apresentacao asc";

             try
            {
                using (var cn = new SqlConnection(_coon))
                {
                    cn.Open();

                    using (var cmd = new SqlCommand(sql, cn))
                    {
                        using (var dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    listaTarefas.Add(new Tarefa(
                                        Convert.ToInt32(dr["cd_tarefa"]),
                                        Convert.ToString(dr["nome_tarefa"]),
                                        Convert.ToDecimal(dr["vlr_custo"]),
                                        Convert.ToDateTime(dr["dt_limite"]),
                                        Convert.ToInt32(dr["nr_ordem_apresentacao"])));
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            { Console.WriteLine("Falha: " + ex.Message); }

            return listaTarefas;
        }

        public void Salvar()
        {
            var sql = "INSERT INTO tb_tarefa (nome_tarefa,vlr_custo,dt_limite) values (@nome_tarefa,@vlr_custo,@dt_limite)";

            try
            {
                using (var cn = new SqlConnection(_coon))
                {
                    cn.Open();

                    using (var cmd = new SqlCommand(sql, cn))
                    {
                        cmd.Parameters.AddWithValue("@nome_tarefa", NomeTarefa);
                        cmd.Parameters.AddWithValue("@vlr_custo", Custo);
                        cmd.Parameters.AddWithValue("@dt_limite", DateLimite);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao salvar a tarefa: " + ex.Message);
            }
        }

        public void Alterar()
        {
            var sql = "UPDATE tb_tarefa SET nome_tarefa = @nome_tarefa, vlr_custo = @vlr_custo, dt_limite = @dt_limite WHERE cd_tarefa = " + CodigoTarefa;

            try
            {
                using (var cn = new SqlConnection(_coon))
                {
                    cn.Open();

                    using (var cmd = new SqlCommand(sql, cn))
                    {
                        cmd.Parameters.AddWithValue("@nome_tarefa", NomeTarefa);
                        cmd.Parameters.AddWithValue("@vlr_custo", Custo);
                        cmd.Parameters.AddWithValue("@dt_limite", DateLimite);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao alterar a tarefa: " + ex.Message);
            }
        }

        public void Excluir()
        {
            var sql = "DELETE tb_tarefa WHERE cd_tarefa = " + CodigoTarefa;

            try
            {
                using (var cn = new SqlConnection(_coon))
                {
                    cn.Open();

                    using (var cmd = new SqlCommand(sql, cn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao exccluir a tarefa: " + ex.Message);
            }
        }

    }
}
