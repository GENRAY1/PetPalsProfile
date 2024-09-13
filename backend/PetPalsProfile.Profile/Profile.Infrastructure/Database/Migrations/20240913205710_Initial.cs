using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Profile.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "profile");

            migrationBuilder.CreateTable(
                name: "pet_type",
                schema: "profile",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_pet_type", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "profile",
                schema: "profile",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    username = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    account_id = table.Column<Guid>(type: "uuid", nullable: false),
                    main_photo = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    description = table.Column<string>(type: "character varying(600)", maxLength: 600, nullable: true),
                    date_of_birth = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_profile", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "pet",
                schema: "profile",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    profile_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    main_photo = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    description = table.Column<string>(type: "character varying(600)", maxLength: 600, nullable: true),
                    date_of_birth = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    type_id = table.Column<Guid>(type: "uuid", nullable: true),
                    breed = table.Column<string>(type: "text", nullable: true),
                    gender = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_pet", x => x.id);
                    table.ForeignKey(
                        name: "fk_pet_pet_type_type_id",
                        column: x => x.type_id,
                        principalSchema: "profile",
                        principalTable: "pet_type",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_pet_profile_profile_id",
                        column: x => x.profile_id,
                        principalSchema: "profile",
                        principalTable: "profile",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "profile_contact",
                schema: "profile",
                columns: table => new
                {
                    profile_id = table.Column<Guid>(type: "uuid", nullable: false),
                    contact_type = table.Column<int>(type: "integer", nullable: false),
                    link = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_profile_contact", x => new { x.profile_id, x.contact_type });
                    table.ForeignKey(
                        name: "fk_profile_contact_profile_profile_id",
                        column: x => x.profile_id,
                        principalSchema: "profile",
                        principalTable: "profile",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "subscription",
                schema: "profile",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    subscriber_id = table.Column<Guid>(type: "uuid", nullable: false),
                    subscribed_to_id = table.Column<Guid>(type: "uuid", nullable: false),
                    subscription_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    unsubscription_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_subscription", x => x.id);
                    table.ForeignKey(
                        name: "fk_subscription_profile_subscribed_to_id",
                        column: x => x.subscribed_to_id,
                        principalSchema: "profile",
                        principalTable: "profile",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_subscription_profile_subscriber_id",
                        column: x => x.subscriber_id,
                        principalSchema: "profile",
                        principalTable: "profile",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_pet_profile_id",
                schema: "profile",
                table: "pet",
                column: "profile_id");

            migrationBuilder.CreateIndex(
                name: "ix_pet_type_id",
                schema: "profile",
                table: "pet",
                column: "type_id");

            migrationBuilder.CreateIndex(
                name: "ix_subscription_subscribed_to_id",
                schema: "profile",
                table: "subscription",
                column: "subscribed_to_id");

            migrationBuilder.CreateIndex(
                name: "ix_subscription_subscriber_id",
                schema: "profile",
                table: "subscription",
                column: "subscriber_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "pet",
                schema: "profile");

            migrationBuilder.DropTable(
                name: "profile_contact",
                schema: "profile");

            migrationBuilder.DropTable(
                name: "subscription",
                schema: "profile");

            migrationBuilder.DropTable(
                name: "pet_type",
                schema: "profile");

            migrationBuilder.DropTable(
                name: "profile",
                schema: "profile");
        }
    }
}
