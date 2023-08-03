﻿// <auto-generated />
using System;
using System.Numerics;
using FerminToroMS.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FerminToroMS.Infrastructure.Migrations
{
    [DbContext(typeof(FerminToroDbContext))]
    [Migration("20230802203759_ModificacionAtributosACiertasEntidades")]
    partial class ModificacionAtributosACiertasEntidades
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("EstudianteEntityModuloEntity", b =>
                {
                    b.Property<Guid>("EstudiantesAprobadosId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ModulosAprobadosId")
                        .HasColumnType("uuid");

                    b.HasKey("EstudiantesAprobadosId", "ModulosAprobadosId");

                    b.HasIndex("ModulosAprobadosId");

                    b.ToTable("EstudianteEntityModuloEntity");
                });

            modelBuilder.Entity("FerminToroMS.Core.Entities.AbonoEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnOrder(1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<Guid>("EstudianteId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("PagoId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("EstudianteId");

                    b.HasIndex("PagoId");

                    b.ToTable("Abonos");
                });

            modelBuilder.Entity("FerminToroMS.Core.Entities.CronogramaEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnOrder(1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<int>("Duracion_Semanas")
                        .HasColumnType("integer");

                    b.Property<DateOnly?>("FechaFin")
                        .HasColumnType("date");

                    b.Property<DateOnly>("FechaInicio")
                        .HasColumnType("date");

                    b.Property<string>("Horario_Dias")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("InstructorId")
                        .HasColumnType("uuid");

                    b.Property<int>("Modalidad")
                        .HasColumnType("integer");

                    b.Property<Guid>("ModuloId")
                        .HasColumnType("uuid");

                    b.Property<int>("NroHoras")
                        .HasColumnType("integer");

                    b.Property<Guid>("PeriodoId")
                        .HasColumnType("uuid");

                    b.Property<int>("Regularidad")
                        .HasColumnType("integer");

                    b.Property<int>("Turno")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("InstructorId");

                    b.HasIndex("ModuloId");

                    b.HasIndex("PeriodoId");

                    b.ToTable("Cronogramas");
                });

            modelBuilder.Entity("FerminToroMS.Core.Entities.CursoEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnOrder(1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Cursos");
                });

            modelBuilder.Entity("FerminToroMS.Core.Entities.DatoEmpresaJuridicaEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnOrder(1);

                    b.Property<string>("Correo_Administrativo")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<string>("NombreEmpresa")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Telefono_Administrativo")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("URL_Rif")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Datos_Empresa_Juridica");
                });

            modelBuilder.Entity("FerminToroMS.Core.Entities.DeudasEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnOrder(1);

                    b.Property<bool>("Aplica_Arancel")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<bool>("Deuda_Vencida")
                        .HasColumnType("boolean");

                    b.Property<Guid>("EstudianteId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("InscripcionId")
                        .HasColumnType("uuid");

                    b.Property<float>("MontoDeuda")
                        .HasColumnType("real");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("EstudianteId");

                    b.HasIndex("InscripcionId");

                    b.ToTable("Deudas");
                });

            modelBuilder.Entity("FerminToroMS.Core.Entities.Empleado_PermisoEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnOrder(1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<Guid>("EmpleadoEntityId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("PermisoEntityId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("PermisoId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("EmpleadoEntityId");

                    b.HasIndex("PermisoId");

                    b.ToTable("Permisos_Empleados");
                });

            modelBuilder.Entity("FerminToroMS.Core.Entities.Fechas_PagoEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnOrder(1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateOnly>("FechaFin")
                        .HasColumnType("date");

                    b.Property<DateOnly>("FechaFinCuota")
                        .HasColumnType("date");

                    b.Property<DateOnly>("FechaInicio")
                        .HasColumnType("date");

                    b.Property<Guid>("PeriodoId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("PeriodoId");

                    b.ToTable("Fechas_Limite_Pago");
                });

            modelBuilder.Entity("FerminToroMS.Core.Entities.InscripcionEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnOrder(1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<Guid>("CronogramaId")
                        .HasColumnType("uuid");

                    b.Property<string>("EstadoSolvencia")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("EstadoVenezuela")
                        .HasColumnType("text");

                    b.Property<bool>("FueraVenezuela")
                        .HasColumnType("boolean");

                    b.Property<string>("NotaAcademica")
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CronogramaId");

                    b.ToTable("Inscripciones");
                });

            modelBuilder.Entity("FerminToroMS.Core.Entities.Metodo_PagoEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnOrder(1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<string>("NombreMetodo")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Metodos_Pago");
                });

            modelBuilder.Entity("FerminToroMS.Core.Entities.ModuloEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnOrder(1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<Guid>("CursoId")
                        .HasColumnType("uuid");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CursoId");

                    b.ToTable("Modulos");
                });

            modelBuilder.Entity("FerminToroMS.Core.Entities.Modulos_AprobadoEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnOrder(1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<Guid>("EstudianteId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ModuloId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("EstudianteId");

                    b.HasIndex("ModuloId");

                    b.ToTable("Modulos_Aprobados");
                });

            modelBuilder.Entity("FerminToroMS.Core.Entities.PagoEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnOrder(1);

                    b.Property<bool?>("CheckRetencion")
                        .HasColumnType("boolean");

                    b.Property<string>("Comentarios")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<Guid?>("EmpresaJuridicaId")
                        .HasColumnType("uuid");

                    b.Property<bool>("EnDivisa")
                        .HasColumnType("boolean");

                    b.Property<bool?>("EsAprobado")
                        .HasColumnType("boolean");

                    b.Property<bool>("EsJuridico")
                        .HasColumnType("boolean");

                    b.Property<bool?>("EsPagoDeAbono")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("FechaPagoEfectivo")
                        .HasColumnType("text");

                    b.Property<TimeOnly?>("HoraPagoEfectivo")
                        .HasColumnType("time without time zone");

                    b.Property<Guid>("InscripcionId")
                        .HasColumnType("uuid");

                    b.Property<int?>("NroFactura")
                        .HasColumnType("integer");

                    b.Property<int?>("NroRecibo")
                        .HasColumnType("integer");

                    b.Property<int?>("NroRetencion")
                        .HasColumnType("integer");

                    b.Property<bool>("PorCuotas")
                        .HasColumnType("boolean");

                    b.Property<Guid?>("PrimeraCuotaId")
                        .HasColumnType("uuid");

                    b.Property<string>("URLComprobante")
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("EmpresaJuridicaId");

                    b.HasIndex("InscripcionId");

                    b.HasIndex("PrimeraCuotaId");

                    b.ToTable("Pagos");
                });

            modelBuilder.Entity("FerminToroMS.Core.Entities.PeriodoEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnOrder(1);

                    b.Property<int>("Año")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<string>("Meses")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("NombrePeriodo")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Periodos");
                });

            modelBuilder.Entity("FerminToroMS.Core.Entities.PermisosEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnOrder(1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<Guid?>("InstructorEntityId")
                        .HasColumnType("uuid");

                    b.Property<string>("NombrePermiso")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("InstructorEntityId");

                    b.ToTable("Permisos");
                });

            modelBuilder.Entity("FerminToroMS.Core.Entities.Precio_Mod_TurnoEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnOrder(1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<Guid?>("CursoEntityId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("CursoId")
                        .HasColumnType("uuid");

                    b.Property<int>("Modalidad")
                        .HasColumnType("integer");

                    b.Property<float>("Precio")
                        .HasColumnType("real");

                    b.Property<int>("Turno")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CursoEntityId");

                    b.ToTable("Precios");
                });

            modelBuilder.Entity("FerminToroMS.Core.Entities.PromocionEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnOrder(1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<Guid?>("CronogramaId")
                        .HasColumnType("uuid");

                    b.Property<int>("Descuento")
                        .HasColumnType("integer");

                    b.Property<DateOnly>("FechaFin")
                        .HasColumnType("date");

                    b.Property<DateOnly>("FechaInicio")
                        .HasColumnType("date");

                    b.Property<string>("NombrePromocion")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CronogramaId");

                    b.ToTable("Promociones");
                });

            modelBuilder.Entity("FerminToroMS.Core.Entities.Usuarios.EmpleadoEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnOrder(1);

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnOrder(4);

                    b.Property<BigInteger>("Cedula")
                        .HasColumnType("numeric")
                        .HasColumnOrder(2);

                    b.Property<string>("Correo")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnOrder(5);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnOrder(3);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Empleados");

                    b.HasDiscriminator<string>("Discriminator").HasValue("EmpleadoEntity");
                });

            modelBuilder.Entity("FerminToroMS.Core.Entities.Usuarios.EstudianteEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnOrder(1);

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnOrder(4);

                    b.Property<BigInteger>("Cedula")
                        .HasColumnType("numeric")
                        .HasColumnOrder(2);

                    b.Property<string>("Codigo_Verificacion")
                        .HasColumnType("text");

                    b.Property<string>("Correo")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnOrder(5);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<string>("Direccion_Hab")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("Edad")
                        .HasColumnType("integer");

                    b.Property<bool>("Es_Regular")
                        .HasColumnType("boolean");

                    b.Property<DateOnly?>("Fecha_Nac")
                        .HasColumnType("date");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnOrder(3);

                    b.Property<int>("Porcentaje_Beca")
                        .HasColumnType("integer");

                    b.Property<string>("Rango_Edad")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Estudiantes");
                });

            modelBuilder.Entity("FerminToroMS.Core.Entities.ValoresEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnOrder(1);

                    b.Property<string>("Apellido")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<string>("Identificacion")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Nombre")
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Valores");
                });

            modelBuilder.Entity("FerminToroMS.Core.Entities.Usuarios.InstructorEntity", b =>
                {
                    b.HasBaseType("FerminToroMS.Core.Entities.Usuarios.EmpleadoEntity");

                    b.HasDiscriminator().HasValue("InstructorEntity");
                });

            modelBuilder.Entity("EstudianteEntityModuloEntity", b =>
                {
                    b.HasOne("FerminToroMS.Core.Entities.Usuarios.EstudianteEntity", null)
                        .WithMany()
                        .HasForeignKey("EstudiantesAprobadosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FerminToroMS.Core.Entities.ModuloEntity", null)
                        .WithMany()
                        .HasForeignKey("ModulosAprobadosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FerminToroMS.Core.Entities.AbonoEntity", b =>
                {
                    b.HasOne("FerminToroMS.Core.Entities.Usuarios.EstudianteEntity", "Estudiante")
                        .WithMany("Abonos")
                        .HasForeignKey("EstudianteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FerminToroMS.Core.Entities.PagoEntity", "Pago")
                        .WithMany("Abonos")
                        .HasForeignKey("PagoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Estudiante");

                    b.Navigation("Pago");
                });

            modelBuilder.Entity("FerminToroMS.Core.Entities.CronogramaEntity", b =>
                {
                    b.HasOne("FerminToroMS.Core.Entities.Usuarios.InstructorEntity", "Instructor")
                        .WithMany("CronogramasAsignados")
                        .HasForeignKey("InstructorId");

                    b.HasOne("FerminToroMS.Core.Entities.ModuloEntity", "Modulo")
                        .WithMany("Cronogramas")
                        .HasForeignKey("ModuloId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FerminToroMS.Core.Entities.PeriodoEntity", "Periodo")
                        .WithMany("Cronogramas")
                        .HasForeignKey("PeriodoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Instructor");

                    b.Navigation("Modulo");

                    b.Navigation("Periodo");
                });

            modelBuilder.Entity("FerminToroMS.Core.Entities.DeudasEntity", b =>
                {
                    b.HasOne("FerminToroMS.Core.Entities.Usuarios.EstudianteEntity", "Estudiante")
                        .WithMany("Deudas")
                        .HasForeignKey("EstudianteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FerminToroMS.Core.Entities.InscripcionEntity", "Inscripcion")
                        .WithMany("Deudas")
                        .HasForeignKey("InscripcionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Estudiante");

                    b.Navigation("Inscripcion");
                });

            modelBuilder.Entity("FerminToroMS.Core.Entities.Empleado_PermisoEntity", b =>
                {
                    b.HasOne("FerminToroMS.Core.Entities.Usuarios.EmpleadoEntity", "Empleado")
                        .WithMany()
                        .HasForeignKey("EmpleadoEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FerminToroMS.Core.Entities.PermisosEntity", "Permiso")
                        .WithMany()
                        .HasForeignKey("PermisoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Empleado");

                    b.Navigation("Permiso");
                });

            modelBuilder.Entity("FerminToroMS.Core.Entities.Fechas_PagoEntity", b =>
                {
                    b.HasOne("FerminToroMS.Core.Entities.PeriodoEntity", "Periodo")
                        .WithMany("Fechas_Pago")
                        .HasForeignKey("PeriodoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Periodo");
                });

            modelBuilder.Entity("FerminToroMS.Core.Entities.InscripcionEntity", b =>
                {
                    b.HasOne("FerminToroMS.Core.Entities.CronogramaEntity", "Cronograma")
                        .WithMany("Inscripciones")
                        .HasForeignKey("CronogramaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cronograma");
                });

            modelBuilder.Entity("FerminToroMS.Core.Entities.ModuloEntity", b =>
                {
                    b.HasOne("FerminToroMS.Core.Entities.CursoEntity", "Curso")
                        .WithMany("Modulos")
                        .HasForeignKey("CursoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Curso");
                });

            modelBuilder.Entity("FerminToroMS.Core.Entities.Modulos_AprobadoEntity", b =>
                {
                    b.HasOne("FerminToroMS.Core.Entities.Usuarios.EstudianteEntity", "Estudiante")
                        .WithMany()
                        .HasForeignKey("EstudianteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FerminToroMS.Core.Entities.ModuloEntity", "Modulo")
                        .WithMany()
                        .HasForeignKey("ModuloId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Estudiante");

                    b.Navigation("Modulo");
                });

            modelBuilder.Entity("FerminToroMS.Core.Entities.PagoEntity", b =>
                {
                    b.HasOne("FerminToroMS.Core.Entities.DatoEmpresaJuridicaEntity", "EmpresaJuridica")
                        .WithMany("PagosJuridicos")
                        .HasForeignKey("EmpresaJuridicaId");

                    b.HasOne("FerminToroMS.Core.Entities.InscripcionEntity", "Inscripcion")
                        .WithMany("Pagos")
                        .HasForeignKey("InscripcionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FerminToroMS.Core.Entities.PagoEntity", "PrimeraCuota")
                        .WithMany()
                        .HasForeignKey("PrimeraCuotaId");

                    b.Navigation("EmpresaJuridica");

                    b.Navigation("Inscripcion");

                    b.Navigation("PrimeraCuota");
                });

            modelBuilder.Entity("FerminToroMS.Core.Entities.PermisosEntity", b =>
                {
                    b.HasOne("FerminToroMS.Core.Entities.Usuarios.InstructorEntity", null)
                        .WithMany("Permisos")
                        .HasForeignKey("InstructorEntityId");
                });

            modelBuilder.Entity("FerminToroMS.Core.Entities.Precio_Mod_TurnoEntity", b =>
                {
                    b.HasOne("FerminToroMS.Core.Entities.CursoEntity", null)
                        .WithMany("Precios")
                        .HasForeignKey("CursoEntityId");
                });

            modelBuilder.Entity("FerminToroMS.Core.Entities.PromocionEntity", b =>
                {
                    b.HasOne("FerminToroMS.Core.Entities.CronogramaEntity", "Cronograma")
                        .WithMany("Promociones")
                        .HasForeignKey("CronogramaId");

                    b.Navigation("Cronograma");
                });

            modelBuilder.Entity("FerminToroMS.Core.Entities.CronogramaEntity", b =>
                {
                    b.Navigation("Inscripciones");

                    b.Navigation("Promociones");
                });

            modelBuilder.Entity("FerminToroMS.Core.Entities.CursoEntity", b =>
                {
                    b.Navigation("Modulos");

                    b.Navigation("Precios");
                });

            modelBuilder.Entity("FerminToroMS.Core.Entities.DatoEmpresaJuridicaEntity", b =>
                {
                    b.Navigation("PagosJuridicos");
                });

            modelBuilder.Entity("FerminToroMS.Core.Entities.InscripcionEntity", b =>
                {
                    b.Navigation("Deudas");

                    b.Navigation("Pagos");
                });

            modelBuilder.Entity("FerminToroMS.Core.Entities.ModuloEntity", b =>
                {
                    b.Navigation("Cronogramas");
                });

            modelBuilder.Entity("FerminToroMS.Core.Entities.PagoEntity", b =>
                {
                    b.Navigation("Abonos");
                });

            modelBuilder.Entity("FerminToroMS.Core.Entities.PeriodoEntity", b =>
                {
                    b.Navigation("Cronogramas");

                    b.Navigation("Fechas_Pago");
                });

            modelBuilder.Entity("FerminToroMS.Core.Entities.Usuarios.EstudianteEntity", b =>
                {
                    b.Navigation("Abonos");

                    b.Navigation("Deudas");
                });

            modelBuilder.Entity("FerminToroMS.Core.Entities.Usuarios.InstructorEntity", b =>
                {
                    b.Navigation("CronogramasAsignados");

                    b.Navigation("Permisos");
                });
#pragma warning restore 612, 618
        }
    }
}
