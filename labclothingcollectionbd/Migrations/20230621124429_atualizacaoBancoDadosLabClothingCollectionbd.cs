using Microsoft.EntityFrameworkCore.Migrations;

namespace labclothingcollection.Migrations
{
    public partial class AtualizacaoBancoDadosLabClothingCollectionbd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Usuarios_Email",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Pessoas_CpfCnpj",
                table: "Pessoas");

            migrationBuilder.DropIndex(
                name: "IX_Modelos_NomeModelo",
                table: "Modelos");

            migrationBuilder.DropIndex(
                name: "IX_Colecoes_NomeColecao",
                table: "Colecoes");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_IdPessoa",
                table: "Usuarios",
                column: "IdPessoa",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pessoas_IdPessoa",
                table: "Pessoas",
                column: "IdPessoa",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Modelos_IdModelo",
                table: "Modelos",
                column: "IdModelo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Colecoes_IdColecaoRelacionada",
                table: "Colecoes",
                column: "IdColecaoRelacionada",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Usuarios_IdPessoa",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Pessoas_IdPessoa",
                table: "Pessoas");

            migrationBuilder.DropIndex(
                name: "IX_Modelos_IdModelo",
                table: "Modelos");

            migrationBuilder.DropIndex(
                name: "IX_Colecoes_IdColecaoRelacionada",
                table: "Colecoes");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Email",
                table: "Usuarios",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Pessoas_CpfCnpj",
                table: "Pessoas",
                column: "CpfCnpj",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Modelos_NomeModelo",
                table: "Modelos",
                column: "NomeModelo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Colecoes_NomeColecao",
                table: "Colecoes",
                column: "NomeColecao",
                unique: true);
        }
    }
}
