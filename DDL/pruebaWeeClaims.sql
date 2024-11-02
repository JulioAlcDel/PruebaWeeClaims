USE master;
GO
IF EXISTS (SELECT name FROM sys.databases WHERE name = 'PruebaWeeClaims')
BEGIN
    ALTER DATABASE PruebaWeeClaims SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE PruebaWeeClaims;
END

-- Crear la base de datos de nuevo
CREATE DATABASE PruebaWeeClaims;
GO
-- Usar la base de datos reci�n creada
USE PruebaWeeClaims;
GO
CREATE TABLE Contactos(
   Id INT PRIMARY KEY IDENTITY(1,1),
   Nombre VARCHAR(500),
   NombreCompania VARCHAR(800),
   Email  VARCHAR(800),
   Telefono VARCHAR(50),
   Codiciones BIT,
   FechaAlta DATETIME DEFAULT CURRENT_TIMESTAMP

)
GO