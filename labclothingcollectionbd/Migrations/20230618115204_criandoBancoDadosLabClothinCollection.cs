using Microsoft.EntityFrameworkCore.Migrations;

namespace labclothingcollection.Migrations
{
    public partial class criandoBancoDadosLabClothinCollection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Colecoes",
                columns: table => new
                {
                    IdColecaoRelacionada = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeColecao = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IdResponsavel = table.Column<int>(type: "int", nullable: false),
                    Marca = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Orcamento = table.Column<int>(type: "int", nullable: false),
                    AnoLancamento = table.Column<int>(type: "int", nullable: false),
                    Estacao = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    EstadoSistema = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colecoes", x => x.IdColecaoRelacionada);
                });

            migrationBuilder.CreateTable(
                name: "Modelos",
                columns: table => new
                {
                    IdModelo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeModelo = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    IdColecaoRelacionada = table.Column<int>(type: "int", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Layout = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modelos", x => x.IdModelo);
                });

            migrationBuilder.CreateTable(
                name: "Pessoas",
                columns: table => new
                {
                    IdPessoa = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeCompleto = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Genero = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    DataNascimento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CpfCnpj = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pessoas", x => x.IdPessoa);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    IdPessoa = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TipoUsuario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstadoUsuario = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.IdPessoa);
                    table.ForeignKey(
                        name: "FK_Usuarios_Pessoas_IdPessoa",
                        column: x => x.IdPessoa,
                        principalTable: "Pessoas",
                        principalColumn: "IdPessoa",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Colecoes_NomeColecao",
                table: "Colecoes",
                column: "NomeColecao",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Modelos_NomeModelo",
                table: "Modelos",
                column: "NomeModelo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pessoas_CpfCnpj",
                table: "Pessoas",
                column: "CpfCnpj",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Email",
                table: "Usuarios",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Colecoes");

            migrationBuilder.DropTable(
                name: "Modelos");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Pessoas");
        }
    }
}
