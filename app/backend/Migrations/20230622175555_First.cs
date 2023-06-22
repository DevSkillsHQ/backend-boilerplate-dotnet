using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class First : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Option",
                columns: table => new
                {
                    Key = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Option", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "Poll",
                columns: table => new
                {
                    PollId = table.Column<string>(type: "TEXT", nullable: false),
                    Question = table.Column<string>(type: "TEXT", nullable: true),
                    OptionsKey = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Poll", x => x.PollId);
                    table.ForeignKey(
                        name: "FK_Poll_Option_OptionsKey",
                        column: x => x.OptionsKey,
                        principalTable: "Option",
                        principalColumn: "Key");
                });

            migrationBuilder.CreateTable(
                name: "Presentation",
                columns: table => new
                {
                    CurrentPollIndex = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PollsId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Presentation", x => x.CurrentPollIndex);
                    table.ForeignKey(
                        name: "FK_Presentation_Poll_PollsId",
                        column: x => x.PollsId,
                        principalTable: "Poll",
                        principalColumn: "PollId");
                });

            migrationBuilder.CreateTable(
                name: "Vote",
                columns: table => new
                {
                    Key = table.Column<string>(type: "TEXT", nullable: false),
                    ClientId = table.Column<string>(type: "TEXT", nullable: true),
                    PollId = table.Column<string>(type: "TEXT", nullable: true),
                    OptionKey = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vote", x => x.Key);
                    table.ForeignKey(
                        name: "FK_Vote_Option_OptionKey",
                        column: x => x.OptionKey,
                        principalTable: "Option",
                        principalColumn: "Key");
                    table.ForeignKey(
                        name: "FK_Vote_Poll_PollId",
                        column: x => x.PollId,
                        principalTable: "Poll",
                        principalColumn: "PollId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Poll_OptionsKey",
                table: "Poll",
                column: "OptionsKey");

            migrationBuilder.CreateIndex(
                name: "IX_Presentation_PollsId",
                table: "Presentation",
                column: "PollsId");

            migrationBuilder.CreateIndex(
                name: "IX_Vote_OptionKey",
                table: "Vote",
                column: "OptionKey");

            migrationBuilder.CreateIndex(
                name: "IX_Vote_PollId",
                table: "Vote",
                column: "PollId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Presentation");

            migrationBuilder.DropTable(
                name: "Vote");

            migrationBuilder.DropTable(
                name: "Poll");

            migrationBuilder.DropTable(
                name: "Option");
        }
    }
}
