using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManagementSystem.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Author",
                columns: table => new
                {
                    Author_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Author_Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Author_NickName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Author", x => x.Author_ID);
                });

            migrationBuilder.CreateTable(
                name: "BannedUser",
                columns: table => new
                {
                    Ban_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Ban_Start_Date = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    Ban_End_Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BannedUser", x => x.Ban_Id);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Category_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category_Name = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Category_Id);
                });

            migrationBuilder.CreateTable(
                name: "College",
                columns: table => new
                {
                    College_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    College_Name = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_College", x => x.College_ID);
                });

            migrationBuilder.CreateTable(
                name: "Language",
                columns: table => new
                {
                    Language_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Language_Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Language", x => x.Language_Id);
                });

            migrationBuilder.CreateTable(
                name: "Library",
                columns: table => new
                {
                    LibraryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Library", x => x.LibraryID);
                });

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    LogId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    TableName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OperationType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.LogId);
                });

            migrationBuilder.CreateTable(
                name: "Publishers",
                columns: table => new
                {
                    Publisher_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Publisher_Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publishers", x => x.Publisher_ID);
                });

            migrationBuilder.CreateTable(
                name: "Subheadings",
                columns: table => new
                {
                    Subheading_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Subheading_Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subheadings", x => x.Subheading_Id);
                });

            migrationBuilder.CreateTable(
                name: "SupplyMethods",
                columns: table => new
                {
                    Supply_Method_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Supply_Method_Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplyMethods", x => x.Supply_Method_Id);
                });

            migrationBuilder.CreateTable(
                name: "Visits",
                columns: table => new
                {
                    Visit_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Visit_Date = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    Visit_Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visits", x => x.Visit_Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    Department_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Department_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    College_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.Department_ID);
                    table.ForeignKey(
                        name: "FK_Department_College_College_ID",
                        column: x => x.College_ID,
                        principalTable: "College",
                        principalColumn: "College_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    User_Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LibraryID = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.User_Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Library_LibraryID",
                        column: x => x.LibraryID,
                        principalTable: "Library",
                        principalColumn: "LibraryID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LibraryBook",
                columns: table => new
                {
                    LibraryBookID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LibraryID = table.Column<int>(type: "int", nullable: false),
                    BookID = table.Column<int>(type: "int", nullable: false),
                    Available_Copies = table.Column<int>(type: "int", nullable: false),
                    Total_Copies = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LibraryBook", x => x.LibraryBookID);
                    table.ForeignKey(
                        name: "FK_LibraryBook_Library_LibraryID",
                        column: x => x.LibraryID,
                        principalTable: "Library",
                        principalColumn: "LibraryID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    BookID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Classification_Number = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ISBN = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Subtitle = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Publish_Year = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsMissing = table.Column<bool>(type: "bit", nullable: false),
                    IsDamage = table.Column<bool>(type: "bit", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CoverImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Attachment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Place_of_publication = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Locator = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsLocked = table.Column<bool>(type: "bit", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Donor_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Reciving_Date = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    Author_Id = table.Column<int>(type: "int", nullable: false),
                    Category_Id = table.Column<int>(type: "int", nullable: false),
                    Supply_Method_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.BookID);
                    table.ForeignKey(
                        name: "FK_Book_Author_Author_Id",
                        column: x => x.Author_Id,
                        principalTable: "Author",
                        principalColumn: "Author_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Book_Category_Category_Id",
                        column: x => x.Category_Id,
                        principalTable: "Category",
                        principalColumn: "Category_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Book_SupplyMethods_Supply_Method_Id",
                        column: x => x.Supply_Method_Id,
                        principalTable: "SupplyMethods",
                        principalColumn: "Supply_Method_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Curriculum",
                columns: table => new
                {
                    Curriculum_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Curriculum_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Department_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Curriculum", x => x.Curriculum_ID);
                    table.ForeignKey(
                        name: "FK_Curriculum_Department_Department_ID",
                        column: x => x.Department_ID,
                        principalTable: "Department",
                        principalColumn: "Department_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "User_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "User_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "User_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "User_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Borrowing",
                columns: table => new
                {
                    Borrowing_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumberOfDays = table.Column<int>(type: "int", nullable: false),
                    Date_Of_Borrowing = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    Return_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Actual_Return_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsReturned = table.Column<bool>(type: "bit", nullable: false),
                    User_Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LibraryBook_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Borrowing", x => x.Borrowing_Id);
                    table.ForeignKey(
                        name: "FK_Borrowing_LibraryBook_LibraryBook_ID",
                        column: x => x.LibraryBook_ID,
                        principalTable: "LibraryBook",
                        principalColumn: "LibraryBookID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Copying",
                columns: table => new
                {
                    Copying_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Start_Time = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    End_Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsReturned = table.Column<bool>(type: "bit", nullable: false),
                    User_Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LibraryBookID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Copying", x => x.Copying_Id);
                    table.ForeignKey(
                        name: "FK_Copying_LibraryBook_LibraryBookID",
                        column: x => x.LibraryBookID,
                        principalTable: "LibraryBook",
                        principalColumn: "LibraryBookID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Discarding",
                columns: table => new
                {
                    Discarding_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Discarding_Reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discarding_Date = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    LibraryBookID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discarding", x => x.Discarding_ID);
                    table.ForeignKey(
                        name: "FK_Discarding_LibraryBook_LibraryBookID",
                        column: x => x.LibraryBookID,
                        principalTable: "LibraryBook",
                        principalColumn: "LibraryBookID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Readings",
                columns: table => new
                {
                    Reading_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Start_Time = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    End_Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsFinished = table.Column<bool>(type: "bit", nullable: false),
                    User_Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LibraryBookID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Readings", x => x.Reading_Id);
                    table.ForeignKey(
                        name: "FK_Readings_LibraryBook_LibraryBookID",
                        column: x => x.LibraryBookID,
                        principalTable: "LibraryBook",
                        principalColumn: "LibraryBookID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Transferring",
                columns: table => new
                {
                    Transferring_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Transferring_Date = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    SourceLibraryBookID = table.Column<int>(type: "int", nullable: false),
                    DestinationLibraryBookID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transferring", x => x.Transferring_ID);
                    table.ForeignKey(
                        name: "FK_Transferring_LibraryBook_DestinationLibraryBookID",
                        column: x => x.DestinationLibraryBookID,
                        principalTable: "LibraryBook",
                        principalColumn: "LibraryBookID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transferring_LibraryBook_SourceLibraryBookID",
                        column: x => x.SourceLibraryBookID,
                        principalTable: "LibraryBook",
                        principalColumn: "LibraryBookID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Book_College",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Book_ID = table.Column<int>(type: "int", nullable: false),
                    BookID = table.Column<int>(type: "int", nullable: true),
                    College_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book_College", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Book_College_Book_Book_ID",
                        column: x => x.Book_ID,
                        principalTable: "Book",
                        principalColumn: "BookID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Book_College_Book_BookID",
                        column: x => x.BookID,
                        principalTable: "Book",
                        principalColumn: "BookID");
                    table.ForeignKey(
                        name: "FK_Book_College_College_College_ID",
                        column: x => x.College_ID,
                        principalTable: "College",
                        principalColumn: "College_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Book_Publishers",
                columns: table => new
                {
                    BooksBookID = table.Column<int>(type: "int", nullable: false),
                    PublishersPublisher_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book_Publishers", x => new { x.BooksBookID, x.PublishersPublisher_ID });
                    table.ForeignKey(
                        name: "FK_Book_Publishers_Book_BooksBookID",
                        column: x => x.BooksBookID,
                        principalTable: "Book",
                        principalColumn: "BookID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Book_Publishers_Publishers_PublishersPublisher_ID",
                        column: x => x.PublishersPublisher_ID,
                        principalTable: "Publishers",
                        principalColumn: "Publisher_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Book_Subheadings",
                columns: table => new
                {
                    BookSubheadingsSubheading_Id = table.Column<int>(type: "int", nullable: false),
                    booksBookID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book_Subheadings", x => new { x.BookSubheadingsSubheading_Id, x.booksBookID });
                    table.ForeignKey(
                        name: "FK_Book_Subheadings_Book_booksBookID",
                        column: x => x.booksBookID,
                        principalTable: "Book",
                        principalColumn: "BookID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Book_Subheadings_Subheadings_BookSubheadingsSubheading_Id",
                        column: x => x.BookSubheadingsSubheading_Id,
                        principalTable: "Subheadings",
                        principalColumn: "Subheading_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookLanguage",
                columns: table => new
                {
                    LanguagesLanguage_Id = table.Column<int>(type: "int", nullable: false),
                    booksBookID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookLanguage", x => new { x.LanguagesLanguage_Id, x.booksBookID });
                    table.ForeignKey(
                        name: "FK_BookLanguage_Book_booksBookID",
                        column: x => x.booksBookID,
                        principalTable: "Book",
                        principalColumn: "BookID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookLanguage_Language_LanguagesLanguage_Id",
                        column: x => x.LanguagesLanguage_Id,
                        principalTable: "Language",
                        principalColumn: "Language_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookLibraryBook",
                columns: table => new
                {
                    BookID = table.Column<int>(type: "int", nullable: false),
                    LibraryBookID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookLibraryBook", x => new { x.BookID, x.LibraryBookID });
                    table.ForeignKey(
                        name: "FK_BookLibraryBook_Book_BookID",
                        column: x => x.BookID,
                        principalTable: "Book",
                        principalColumn: "BookID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookLibraryBook_LibraryBook_LibraryBookID",
                        column: x => x.LibraryBookID,
                        principalTable: "LibraryBook",
                        principalColumn: "LibraryBookID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Penalties",
                columns: table => new
                {
                    Penalty_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    Penalty_Date = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    Borrowing_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Penalties", x => x.Penalty_Id);
                    table.ForeignKey(
                        name: "FK_Penalties_Borrowing_Borrowing_Id",
                        column: x => x.Borrowing_Id,
                        principalTable: "Borrowing",
                        principalColumn: "Borrowing_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_LibraryID",
                table: "AspNetUsers",
                column: "LibraryID");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BannedUser_User_Id",
                table: "BannedUser",
                column: "User_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Book_Author_Id",
                table: "Book",
                column: "Author_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Book_Category_Id",
                table: "Book",
                column: "Category_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Book_Supply_Method_Id",
                table: "Book",
                column: "Supply_Method_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Book_College_Book_ID",
                table: "Book_College",
                column: "Book_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Book_College_BookID",
                table: "Book_College",
                column: "BookID");

            migrationBuilder.CreateIndex(
                name: "IX_Book_College_College_ID",
                table: "Book_College",
                column: "College_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Book_Publishers_PublishersPublisher_ID",
                table: "Book_Publishers",
                column: "PublishersPublisher_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Book_Subheadings_booksBookID",
                table: "Book_Subheadings",
                column: "booksBookID");

            migrationBuilder.CreateIndex(
                name: "IX_BookLanguage_booksBookID",
                table: "BookLanguage",
                column: "booksBookID");

            migrationBuilder.CreateIndex(
                name: "IX_BookLibraryBook_LibraryBookID",
                table: "BookLibraryBook",
                column: "LibraryBookID");

            migrationBuilder.CreateIndex(
                name: "IX_Borrowing_LibraryBook_ID",
                table: "Borrowing",
                column: "LibraryBook_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Borrowing_User_Id",
                table: "Borrowing",
                column: "User_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Copying_LibraryBookID",
                table: "Copying",
                column: "LibraryBookID");

            migrationBuilder.CreateIndex(
                name: "IX_Copying_User_Id",
                table: "Copying",
                column: "User_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Curriculum_Department_ID",
                table: "Curriculum",
                column: "Department_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Department_College_ID",
                table: "Department",
                column: "College_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Discarding_LibraryBookID",
                table: "Discarding",
                column: "LibraryBookID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LibraryBook_LibraryID",
                table: "LibraryBook",
                column: "LibraryID");

            migrationBuilder.CreateIndex(
                name: "IX_Penalties_Borrowing_Id",
                table: "Penalties",
                column: "Borrowing_Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Readings_LibraryBookID",
                table: "Readings",
                column: "LibraryBookID");

            migrationBuilder.CreateIndex(
                name: "IX_Readings_User_Id",
                table: "Readings",
                column: "User_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Transferring_DestinationLibraryBookID",
                table: "Transferring",
                column: "DestinationLibraryBookID");

            migrationBuilder.CreateIndex(
                name: "IX_Transferring_SourceLibraryBookID",
                table: "Transferring",
                column: "SourceLibraryBookID");

            migrationBuilder.CreateIndex(
                name: "IX_Visits_User_Id",
                table: "Visits",
                column: "User_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "BannedUser");

            migrationBuilder.DropTable(
                name: "Book_College");

            migrationBuilder.DropTable(
                name: "Book_Publishers");

            migrationBuilder.DropTable(
                name: "Book_Subheadings");

            migrationBuilder.DropTable(
                name: "BookLanguage");

            migrationBuilder.DropTable(
                name: "BookLibraryBook");

            migrationBuilder.DropTable(
                name: "Copying");

            migrationBuilder.DropTable(
                name: "Curriculum");

            migrationBuilder.DropTable(
                name: "Discarding");

            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DropTable(
                name: "Penalties");

            migrationBuilder.DropTable(
                name: "Readings");

            migrationBuilder.DropTable(
                name: "Transferring");

            migrationBuilder.DropTable(
                name: "Visits");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Publishers");

            migrationBuilder.DropTable(
                name: "Subheadings");

            migrationBuilder.DropTable(
                name: "Language");

            migrationBuilder.DropTable(
                name: "Book");

            migrationBuilder.DropTable(
                name: "Department");

            migrationBuilder.DropTable(
                name: "Borrowing");

            migrationBuilder.DropTable(
                name: "Author");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "SupplyMethods");

            migrationBuilder.DropTable(
                name: "College");

            migrationBuilder.DropTable(
                name: "LibraryBook");

            migrationBuilder.DropTable(
                name: "Library");
        }
    }
}
