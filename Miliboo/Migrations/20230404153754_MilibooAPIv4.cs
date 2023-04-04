using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Miliboo.Migrations
{
    public partial class MilibooAPIv4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<int>(
                name: "SEQ_Account",
                startValue: 101L);

            migrationBuilder.CreateSequence<int>(
                name: "SEQ_Address",
                startValue: 251L);

            migrationBuilder.CreateSequence<int>(
                name: "SEQ_Color",
                startValue: 23L);

            migrationBuilder.CreateSequence<int>(
                name: "SEQ_Comment",
                startValue: 14L);

            migrationBuilder.CreateSequence<int>(
                name: "SEQ_CompositeProduct",
                startValue: 18L);

            migrationBuilder.CreateSequence<int>(
                name: "SEQ_Concerned",
                startValue: 39L);

            migrationBuilder.CreateSequence<int>(
                name: "SEQ_Country",
                startValue: 215L);

            migrationBuilder.CreateSequence<int>(
                name: "SEQ_CreditCard",
                startValue: 39L);

            migrationBuilder.CreateSequence<int>(
                name: "SEQ_DeliveryAdress",
                startValue: 35L);

            migrationBuilder.CreateSequence<int>(
                name: "SEQ_DeliveryMethod",
                startValue: 4L);

            migrationBuilder.CreateSequence<int>(
                name: "SEQ_Discount",
                startValue: 6L);

            migrationBuilder.CreateSequence<int>(
                name: "SEQ_Filter",
                startValue: 50L);

            migrationBuilder.CreateSequence<int>(
                name: "SEQ_FilterCategory",
                startValue: 37L);

            migrationBuilder.CreateSequence<int>(
                name: "SEQ_Grouping",
                startValue: 4L);

            migrationBuilder.CreateSequence<int>(
                name: "SEQ_IsFiltered",
                startValue: 79L);

            migrationBuilder.CreateSequence<int>(
                name: "SEQ_Order",
                startValue: 38L);

            migrationBuilder.CreateSequence<int>(
                name: "SEQ_PaymentMethod",
                startValue: 4L);

            migrationBuilder.CreateSequence<int>(
                name: "SEQ_Photo",
                startValue: 66L);

            migrationBuilder.CreateSequence<int>(
                name: "SEQ_Product",
                startValue: 51L);

            migrationBuilder.CreateSequence<int>(
                name: "SEQ_ProductCategory",
                startValue: 39L);

            migrationBuilder.CreateSequence<int>(
                name: "SEQ_ProductType",
                startValue: 18L);

            migrationBuilder.CreateSequence<int>(
                name: "SEQ_Regroup",
                startValue: 20L);

            migrationBuilder.CreateSequence<int>(
                name: "SEQ_StateOrder",
                startValue: 9L);

            migrationBuilder.CreateSequence<int>(
                name: "SEQ_TechnicalAspect",
                startValue: 20L);

            migrationBuilder.CreateTable(
                name: "t_e_account_act",
                columns: table => new
                {
                    act_id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval(SEQ_Account)"),
                    act_password = table.Column<string>(type: "varchar", maxLength: 300, nullable: false),
                    act_firstname = table.Column<string>(type: "varchar", maxLength: 20, nullable: false),
                    act_lastname = table.Column<string>(type: "varchar", maxLength: 20, nullable: false),
                    act_mail = table.Column<string>(type: "varchar", maxLength: 50, nullable: false),
                    act_phonenumber = table.Column<string>(type: "char(10)", nullable: true),
                    act_oath = table.Column<bool>(type: "bool", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_accountid", x => x.act_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_color_clr",
                columns: table => new
                {
                    cl_id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval(SEQ_Color)"),
                    clr_colorHexaCode = table.Column<string>(type: "varchar(10)", nullable: true),
                    clr_colorName = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_color", x => x.cl_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_country_cnt",
                columns: table => new
                {
                    cnt_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    cnt_wording = table.Column<string>(type: "varchar", maxLength: 100, nullable: false),
                    cnt_phonecode = table.Column<string>(type: "varchar", maxLength: 4, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_countryid", x => x.cnt_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_deliveryadress_dla",
                columns: table => new
                {
                    dla_iddeliveryadress = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval(SEQ_DeliveryAdress)"),
                    act_id = table.Column<int>(type: "integer", nullable: false),
                    dla_favadressname = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_deliveryadress", x => x.dla_iddeliveryadress);
                });

            migrationBuilder.CreateTable(
                name: "t_e_deliverymethod_dlm",
                columns: table => new
                {
                    dlm_id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval(SEQ_DeliveryMethod)"),
                    dlm_description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_deliverymethod", x => x.dlm_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_discount_dsc",
                columns: table => new
                {
                    dsc_discountid = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval(SEQ_Discount)"),
                    dsc_discountname = table.Column<string>(type: "varchar", nullable: false),
                    dsc_isactive = table.Column<bool>(type: "bool", nullable: false),
                    dsc_value = table.Column<decimal>(type: "decimal", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_discount", x => x.dsc_discountid);
                });

            migrationBuilder.CreateTable(
                name: "t_e_filtercategory_fca",
                columns: table => new
                {
                    fca_id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval(SEQ_FilterCategory)"),
                    fca_filterCategoryName = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_filtercategory", x => x.fca_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_grouping_grp",
                columns: table => new
                {
                    grp_id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval(SEQ_Grouping)"),
                    grp_groupingName = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_grouping", x => x.grp_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_paymentmethod_pay",
                columns: table => new
                {
                    pay_paymentmethodid = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval(SEQ_PaymentMethod)"),
                    pay_methodname = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_paymentmethod", x => x.pay_paymentmethodid);
                });

            migrationBuilder.CreateTable(
                name: "t_e_productcategory_prc",
                columns: table => new
                {
                    prc_id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval(SEQ_ProductCategory)"),
                    prc_parentCategoryID = table.Column<int>(type: "integer", nullable: true),
                    prc_productCategoryName = table.Column<string>(type: "varchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_productcategory", x => x.prc_id);
                    table.ForeignKey(
                        name: "fk_productcat_productcat",
                        column: x => x.prc_parentCategoryID,
                        principalTable: "t_e_productcategory_prc",
                        principalColumn: "prc_id");
                });

            migrationBuilder.CreateTable(
                name: "t_e_producttype_prt",
                columns: table => new
                {
                    prt_id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval(SEQ_ProductType)"),
                    prt_productTypeName = table.Column<string>(type: "varchar(100)", nullable: false),
                    prt_maintenanceCommentPT = table.Column<string>(type: "varchar(500)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_producttype", x => x.prt_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_stateorder_sto",
                columns: table => new
                {
                    sto_stateorderid = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval(SEQ_StateOrder)"),
                    sto_stateordername = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_stateorder", x => x.sto_stateorderid);
                });

            migrationBuilder.CreateTable(
                name: "t_e_technicalaspect_tas",
                columns: table => new
                {
                    tas_id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval(SEQ_TechnicalAspect)"),
                    tas_technicalAspectName = table.Column<string>(type: "varchar(500)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_technicalaspect", x => x.tas_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_creditcard_crc",
                columns: table => new
                {
                    crc_cardid = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval(SEQ_CreditCard)"),
                    crc_accountid = table.Column<int>(type: "integer", nullable: false),
                    crc_name = table.Column<string>(type: "varchar(50)", nullable: true),
                    crc_firstname = table.Column<string>(type: "varchar(50)", nullable: true),
                    crc_expirationdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    crc_cardnumber = table.Column<string>(type: "varchar(50)", nullable: false),
                    crc_cryptogram = table.Column<string>(type: "varchar(3)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_creditcard", x => x.crc_cardid);
                    table.CheckConstraint("CK_CRC_CRYPTOGRAM", "crc_cryptogram::text ~ '^[0-9]{3}$'::text");
                    table.CheckConstraint("Ck_creditcard_date", "crc_expirationdate > now()");
                    table.ForeignKey(
                        name: "FK_t_e_creditcard_crc_t_e_account_act_crc_accountid",
                        column: x => x.crc_accountid,
                        principalTable: "t_e_account_act",
                        principalColumn: "act_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_e_address_adr",
                columns: table => new
                {
                    adr_id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval(SEQ_Address)"),
                    cnt_id = table.Column<int>(type: "integer", nullable: false),
                    adr_wording = table.Column<string>(type: "varchar", nullable: false),
                    adr_postalcode = table.Column<string>(type: "varchar", nullable: false),
                    adr_city = table.Column<string>(type: "varchar", nullable: false),
                    adr_longitude = table.Column<decimal>(type: "numeric(38,17)", nullable: true),
                    adr_latitude = table.Column<decimal>(type: "numeric(38,17)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_addressid", x => x.adr_id);
                    table.CheckConstraint("CK_ADDR_POSTALCODE", "adr_postalcode::text ~ '^[0-9]{5}$'::text");
                    table.ForeignKey(
                        name: "fk_adr_cnt",
                        column: x => x.cnt_id,
                        principalTable: "t_e_country_cnt",
                        principalColumn: "cnt_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_e_filter_flt",
                columns: table => new
                {
                    flt_id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval(SEQ_Filter)"),
                    FilterCategoryId = table.Column<int>(type: "integer", nullable: false),
                    flt_filterName = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_filter", x => x.flt_id);
                    table.ForeignKey(
                        name: "fk_filter_filtercategory",
                        column: x => x.FilterCategoryId,
                        principalTable: "t_e_filtercategory_fca",
                        principalColumn: "fca_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_j_asfilter_aft",
                columns: table => new
                {
                    fca_id = table.Column<int>(type: "integer", nullable: false),
                    prc_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asfilter", x => new { x.fca_id, x.prc_id });
                    table.ForeignKey(
                        name: "fk_filtercat_asfilter",
                        column: x => x.fca_id,
                        principalTable: "t_e_filtercategory_fca",
                        principalColumn: "fca_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_productcat_asfilter",
                        column: x => x.prc_id,
                        principalTable: "t_e_productcategory_prc",
                        principalColumn: "prc_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_e_comment_cmt",
                columns: table => new
                {
                    cmt_id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval(SEQ_Comment)"),
                    act_id = table.Column<int>(type: "integer", nullable: false),
                    prt_id = table.Column<int>(type: "integer", nullable: false),
                    cmt_title = table.Column<string>(type: "varchar", nullable: false),
                    cmt_mark = table.Column<int>(type: "integer", nullable: false),
                    cmt_description = table.Column<string>(type: "varchar", nullable: false),
                    cmt_date = table.Column<DateTime>(type: "date", nullable: false, defaultValue: new DateTime(2023, 4, 4, 17, 37, 54, 227, DateTimeKind.Local).AddTicks(5852)),
                    cmt_answer = table.Column<string>(type: "varchar(500)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_commentid", x => x.cmt_id);
                    table.CheckConstraint("CK_COMMENT_MARK", "cmt_mark >=0 AND cmt_mark <=4");
                    table.ForeignKey(
                        name: "fk_comments_account",
                        column: x => x.act_id,
                        principalTable: "t_e_account_act",
                        principalColumn: "act_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_productcat_asfilter",
                        column: x => x.prt_id,
                        principalTable: "t_e_producttype_prt",
                        principalColumn: "prt_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_e_product_prd",
                columns: table => new
                {
                    prd_id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval(SEQ_Product)"),
                    ColorId = table.Column<int>(type: "integer", nullable: false),
                    ProducTypeId = table.Column<int>(type: "integer", nullable: false),
                    ProductCategoryId = table.Column<int>(type: "integer", nullable: false),
                    prd_productName = table.Column<string>(type: "varchar(100)", nullable: true),
                    prd_productDescription = table.Column<string>(type: "varchar", maxLength: 2000, nullable: true),
                    prd_productPrice = table.Column<double>(type: "double precision", nullable: false),
                    prd_productDiscount = table.Column<double>(type: "double precision", nullable: false),
                    prd_nbStockProduct = table.Column<int>(type: "integer", nullable: false),
                    prd_nbReservedProduct = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product", x => x.prd_id);
                    table.ForeignKey(
                        name: "fk_product_catproduct",
                        column: x => x.ProductCategoryId,
                        principalTable: "t_e_productcategory_prc",
                        principalColumn: "prc_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_product_color",
                        column: x => x.ColorId,
                        principalTable: "t_e_color_clr",
                        principalColumn: "cl_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_product_typeproduct",
                        column: x => x.ProducTypeId,
                        principalTable: "t_e_producttype_prt",
                        principalColumn: "prt_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_j_asaspect_asa",
                columns: table => new
                {
                    prt_id = table.Column<int>(type: "integer", nullable: false),
                    tas_id = table.Column<int>(type: "integer", nullable: false),
                    tas_technicalAspectName = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asaspect", x => new { x.prt_id, x.tas_id });
                    table.ForeignKey(
                        name: "fk_producttype_asaspect",
                        column: x => x.prt_id,
                        principalTable: "t_e_producttype_prt",
                        principalColumn: "prt_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_techaspect_asaspect",
                        column: x => x.tas_id,
                        principalTable: "t_e_technicalaspect_tas",
                        principalColumn: "tas_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_e_order_ord",
                columns: table => new
                {
                    sto_stateorderid = table.Column<int>(type: "integer", nullable: false),
                    ord_id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval(SEQ_Order)"),
                    dlv_iddeliverymethod = table.Column<int>(type: "integer", nullable: false),
                    crc_cardid = table.Column<int>(type: "integer", nullable: false),
                    act_id = table.Column<int>(type: "integer", nullable: false),
                    pay_paymentmethodid = table.Column<int>(type: "integer", nullable: false),
                    dla_iddeliveryadress = table.Column<int>(type: "integer", nullable: false),
                    dsc_discountid = table.Column<int>(type: "integer", nullable: true),
                    ord_name = table.Column<string>(type: "varchar(50)", nullable: true),
                    ord_firstname = table.Column<string>(type: "varchar(50)", nullable: true),
                    ord_phone = table.Column<string>(type: "varchar(20)", nullable: true),
                    ord_cellphone = table.Column<string>(type: "varchar(20)", nullable: true),
                    ord_company = table.Column<string>(type: "varchar(50)", nullable: true),
                    ord_adressadditional = table.Column<string>(type: "varchar(200)", nullable: true),
                    ord_instructions = table.Column<string>(type: "varchar(200)", nullable: true),
                    ord_date = table.Column<DateTime>(type: "date", nullable: false, defaultValue: new DateTime(2023, 4, 4, 17, 37, 54, 227, DateTimeKind.Local).AddTicks(6640)),
                    ord_deliveryprice = table.Column<float>(type: "real", nullable: false, defaultValue: 0f),
                    ord_sms = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_e_order_ord", x => x.ord_id);
                    table.CheckConstraint("Ck_order_date", "ord_date > now()");
                    table.ForeignKey(
                        name: "fk_order_account",
                        column: x => x.act_id,
                        principalTable: "t_e_account_act",
                        principalColumn: "act_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_order_creditcard",
                        column: x => x.crc_cardid,
                        principalTable: "t_e_creditcard_crc",
                        principalColumn: "crc_cardid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_order_deliveryaddress",
                        column: x => x.dla_iddeliveryadress,
                        principalTable: "t_e_deliveryadress_dla",
                        principalColumn: "dla_iddeliveryadress",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_order_deliverymethod",
                        column: x => x.dlv_iddeliverymethod,
                        principalTable: "t_e_deliverymethod_dlm",
                        principalColumn: "dlm_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_order_discount",
                        column: x => x.dsc_discountid,
                        principalTable: "t_e_discount_dsc",
                        principalColumn: "dsc_discountid");
                    table.ForeignKey(
                        name: "fk_order_paymentmethod",
                        column: x => x.pay_paymentmethodid,
                        principalTable: "t_e_paymentmethod_pay",
                        principalColumn: "pay_paymentmethodid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_order_stateorder",
                        column: x => x.sto_stateorderid,
                        principalTable: "t_e_stateorder_sto",
                        principalColumn: "sto_stateorderid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_j_owning_own",
                columns: table => new
                {
                    AccountID = table.Column<int>(type: "integer", nullable: false),
                    AddressID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_owning_addressid_accountid", x => new { x.AddressID, x.AccountID });
                    table.ForeignKey(
                        name: "fk_owning_account",
                        column: x => x.AccountID,
                        principalTable: "t_e_account_act",
                        principalColumn: "act_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_owning_address",
                        column: x => x.AddressID,
                        principalTable: "t_e_address_adr",
                        principalColumn: "adr_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_e_compositeproduct_cpr",
                columns: table => new
                {
                    cpr_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    prd_id = table.Column<int>(type: "integer", nullable: false),
                    cpr_compositeproductid = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval(SEQ_CompositeproductID)"),
                    cpr_compositedescription = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_e_compositeproduct_cpr", x => x.cpr_id);
                    table.ForeignKey(
                        name: "fk_product_productcomposite",
                        column: x => x.prd_id,
                        principalTable: "t_e_product_prd",
                        principalColumn: "prd_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_e_photo_pht",
                columns: table => new
                {
                    pht_id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval(SEQ_Photo)"),
                    ProductId = table.Column<int>(type: "integer", nullable: true),
                    CommentID = table.Column<int>(type: "integer", nullable: true),
                    pht_link = table.Column<string>(type: "varchar(200)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_photo_photoid", x => x.pht_id);
                    table.CheckConstraint("CK_PHOTO_LINK", "pht_link::text ~ '^.*.(?:jpg|gif|png|webm|jpeg|ico|webp)$'::text");
                    table.ForeignKey(
                        name: "fk_photo_comment",
                        column: x => x.CommentID,
                        principalTable: "t_e_comment_cmt",
                        principalColumn: "cmt_id");
                    table.ForeignKey(
                        name: "fk_photo_product",
                        column: x => x.ProductId,
                        principalTable: "t_e_product_prd",
                        principalColumn: "prd_id");
                });

            migrationBuilder.CreateTable(
                name: "t_j_isfiltered_ift",
                columns: table => new
                {
                    ift_id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval(SEQ_IsFiltered)"),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    FilterId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_isfiltered", x => x.ift_id);
                    table.ForeignKey(
                        name: "fk_filter_isfiltered",
                        column: x => x.FilterId,
                        principalTable: "t_e_filter_flt",
                        principalColumn: "flt_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_product_isfiltered",
                        column: x => x.ProductId,
                        principalTable: "t_e_product_prd",
                        principalColumn: "prd_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_j_regroup_rgp",
                columns: table => new
                {
                    grp_id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval(SEQ_Regroup)"),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    GroupingId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_regroup", x => x.grp_id);
                    table.ForeignKey(
                        name: "fk_regroup_grouping",
                        column: x => x.GroupingId,
                        principalTable: "t_e_grouping_grp",
                        principalColumn: "grp_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_regroup_product",
                        column: x => x.ProductId,
                        principalTable: "t_e_product_prd",
                        principalColumn: "prd_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_j_concerned_coc",
                columns: table => new
                {
                    coc_id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval(SEQ_Concerned)"),
                    coc_quantity = table.Column<int>(type: "integer", nullable: false),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    OrderID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_concerned", x => x.coc_id);
                    table.ForeignKey(
                        name: "fk_order_concerned",
                        column: x => x.OrderID,
                        principalTable: "t_e_order_ord",
                        principalColumn: "ord_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_product_concerned",
                        column: x => x.ProductId,
                        principalTable: "t_e_product_prd",
                        principalColumn: "prd_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_t_e_account_act_act_mail",
                table: "t_e_account_act",
                column: "act_mail",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_t_e_address_adr_cnt_id",
                table: "t_e_address_adr",
                column: "cnt_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_comment_cmt_act_id",
                table: "t_e_comment_cmt",
                column: "act_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_comment_cmt_prt_id",
                table: "t_e_comment_cmt",
                column: "prt_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_compositeproduct_cpr_prd_id",
                table: "t_e_compositeproduct_cpr",
                column: "prd_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_creditcard_crc_crc_accountid",
                table: "t_e_creditcard_crc",
                column: "crc_accountid");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_filter_flt_FilterCategoryId",
                table: "t_e_filter_flt",
                column: "FilterCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_order_ord_act_id",
                table: "t_e_order_ord",
                column: "act_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_order_ord_crc_cardid",
                table: "t_e_order_ord",
                column: "crc_cardid");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_order_ord_dla_iddeliveryadress",
                table: "t_e_order_ord",
                column: "dla_iddeliveryadress");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_order_ord_dlv_iddeliverymethod",
                table: "t_e_order_ord",
                column: "dlv_iddeliverymethod");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_order_ord_dsc_discountid",
                table: "t_e_order_ord",
                column: "dsc_discountid");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_order_ord_pay_paymentmethodid",
                table: "t_e_order_ord",
                column: "pay_paymentmethodid");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_order_ord_sto_stateorderid",
                table: "t_e_order_ord",
                column: "sto_stateorderid");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_photo_pht_CommentID",
                table: "t_e_photo_pht",
                column: "CommentID");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_photo_pht_ProductId",
                table: "t_e_photo_pht",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_product_prd_ColorId",
                table: "t_e_product_prd",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_product_prd_ProductCategoryId",
                table: "t_e_product_prd",
                column: "ProductCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_product_prd_ProducTypeId",
                table: "t_e_product_prd",
                column: "ProducTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_productcategory_prc_prc_parentCategoryID",
                table: "t_e_productcategory_prc",
                column: "prc_parentCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_asaspect_asa_tas_id",
                table: "t_j_asaspect_asa",
                column: "tas_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_asfilter_aft_prc_id",
                table: "t_j_asfilter_aft",
                column: "prc_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_concerned_coc_OrderID",
                table: "t_j_concerned_coc",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_concerned_coc_ProductId",
                table: "t_j_concerned_coc",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_isfiltered_ift_FilterId",
                table: "t_j_isfiltered_ift",
                column: "FilterId");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_isfiltered_ift_ProductId",
                table: "t_j_isfiltered_ift",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_owning_own_AccountID",
                table: "t_j_owning_own",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_regroup_rgp_GroupingId",
                table: "t_j_regroup_rgp",
                column: "GroupingId");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_regroup_rgp_ProductId",
                table: "t_j_regroup_rgp",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "t_e_compositeproduct_cpr");

            migrationBuilder.DropTable(
                name: "t_e_photo_pht");

            migrationBuilder.DropTable(
                name: "t_j_asaspect_asa");

            migrationBuilder.DropTable(
                name: "t_j_asfilter_aft");

            migrationBuilder.DropTable(
                name: "t_j_concerned_coc");

            migrationBuilder.DropTable(
                name: "t_j_isfiltered_ift");

            migrationBuilder.DropTable(
                name: "t_j_owning_own");

            migrationBuilder.DropTable(
                name: "t_j_regroup_rgp");

            migrationBuilder.DropTable(
                name: "t_e_comment_cmt");

            migrationBuilder.DropTable(
                name: "t_e_technicalaspect_tas");

            migrationBuilder.DropTable(
                name: "t_e_order_ord");

            migrationBuilder.DropTable(
                name: "t_e_filter_flt");

            migrationBuilder.DropTable(
                name: "t_e_address_adr");

            migrationBuilder.DropTable(
                name: "t_e_grouping_grp");

            migrationBuilder.DropTable(
                name: "t_e_product_prd");

            migrationBuilder.DropTable(
                name: "t_e_creditcard_crc");

            migrationBuilder.DropTable(
                name: "t_e_deliveryadress_dla");

            migrationBuilder.DropTable(
                name: "t_e_deliverymethod_dlm");

            migrationBuilder.DropTable(
                name: "t_e_discount_dsc");

            migrationBuilder.DropTable(
                name: "t_e_paymentmethod_pay");

            migrationBuilder.DropTable(
                name: "t_e_stateorder_sto");

            migrationBuilder.DropTable(
                name: "t_e_filtercategory_fca");

            migrationBuilder.DropTable(
                name: "t_e_country_cnt");

            migrationBuilder.DropTable(
                name: "t_e_productcategory_prc");

            migrationBuilder.DropTable(
                name: "t_e_color_clr");

            migrationBuilder.DropTable(
                name: "t_e_producttype_prt");

            migrationBuilder.DropTable(
                name: "t_e_account_act");

            migrationBuilder.DropSequence(
                name: "SEQ_Account");

            migrationBuilder.DropSequence(
                name: "SEQ_Address");

            migrationBuilder.DropSequence(
                name: "SEQ_Color");

            migrationBuilder.DropSequence(
                name: "SEQ_Comment");

            migrationBuilder.DropSequence(
                name: "SEQ_CompositeProduct");

            migrationBuilder.DropSequence(
                name: "SEQ_Concerned");

            migrationBuilder.DropSequence(
                name: "SEQ_Country");

            migrationBuilder.DropSequence(
                name: "SEQ_CreditCard");

            migrationBuilder.DropSequence(
                name: "SEQ_DeliveryAdress");

            migrationBuilder.DropSequence(
                name: "SEQ_DeliveryMethod");

            migrationBuilder.DropSequence(
                name: "SEQ_Discount");

            migrationBuilder.DropSequence(
                name: "SEQ_Filter");

            migrationBuilder.DropSequence(
                name: "SEQ_FilterCategory");

            migrationBuilder.DropSequence(
                name: "SEQ_Grouping");

            migrationBuilder.DropSequence(
                name: "SEQ_IsFiltered");

            migrationBuilder.DropSequence(
                name: "SEQ_Order");

            migrationBuilder.DropSequence(
                name: "SEQ_PaymentMethod");

            migrationBuilder.DropSequence(
                name: "SEQ_Photo");

            migrationBuilder.DropSequence(
                name: "SEQ_Product");

            migrationBuilder.DropSequence(
                name: "SEQ_ProductCategory");

            migrationBuilder.DropSequence(
                name: "SEQ_ProductType");

            migrationBuilder.DropSequence(
                name: "SEQ_Regroup");

            migrationBuilder.DropSequence(
                name: "SEQ_StateOrder");

            migrationBuilder.DropSequence(
                name: "SEQ_TechnicalAspect");
        }
    }
}
