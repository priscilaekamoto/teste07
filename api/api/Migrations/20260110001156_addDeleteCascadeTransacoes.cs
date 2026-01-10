using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class addDeleteCascadeTransacoes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transacoes_Pessoas_PessoaId",
                table: "Transacoes");

            migrationBuilder.AddForeignKey(
                name: "FK_Transacoes_Pessoas_PessoaId",
                table: "Transacoes",
                column: "PessoaId",
                principalTable: "Pessoas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transacoes_Pessoas_PessoaId",
                table: "Transacoes");

            migrationBuilder.AddForeignKey(
                name: "FK_Transacoes_Pessoas_PessoaId",
                table: "Transacoes",
                column: "PessoaId",
                principalTable: "Pessoas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
