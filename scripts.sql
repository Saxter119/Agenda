CREATE TABLE Contactos
(
    id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    nombre NVARCHAR(100) NOT NULL,
	apellido NVARCHAR(100) NULL,
	cedula NVARCHAR(13) NULL,
	direccion NVARCHAR(255) NULL,
	eliminado BIT NUll
);

CREATE TABLE Telefonos(

	id INT IDENTITY(1,1) PRIMARY KEY,
	telefono NVARCHAR(15) NOT NULL,
	usuarioId INT NOT NULL, FOREIGN KEY (usuarioId) REFERENCES Contactos(id)

); 

CREATE TABLE Correos(

	id INT IDENTITY(1,1) PRIMARY KEY,
	correo NVARCHAr(15) NOT NULL,
	usuarioId INT NOT NULL, 
	FOREIGN KEY (usuarioId) REFERENCES Contactos(id)

); 