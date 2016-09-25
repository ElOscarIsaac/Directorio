CREATE DATABASE Directorio;
USE Directorio;
CREATE TABLE Usuarios(
	UsuarioID INT PRIMARY KEY AUTO_INCREMENT,
	vchNombreEmpleado VARCHAR(100) NOT NULL,
    vchNombreUsuario VARCHAR(100) NOT NULL,
    vchPassword VARCHAR(100) NOT NULL,
    vchCorreoElectronico VARCHAR(100) NOT NULL,
    datFechaRegistro DATETIME NOT NULL,
    bitActivo BIT NOT NULL
);
INSERT INTO Usuarios(vchNombreEmpleado, vchNombreUsuario, vchPassword, vchCorreoElectronico, datFechaRegistro, bitActivo) VALUES(
	'LAZARO ADRIAN GONZALEZ MONTOYA', 'lgonzalez', 'livsolskjaer', 'lgonzalez@fujifilm.com.mx', CURDATE(), 1
);
CREATE TABLE Ubicacion(
	UbicacionID INT PRIMARY KEY AUTO_INCREMENT,
    vchDescripcion VARCHAR(100) NOT NULL,
    bitActivo BIT NOT NULL
);
CREATE TABLE Departamento(
	DepartamentoID INT PRIMARY KEY AUTO_INCREMENT,
    vchDescripcion VARCHAR(100) NOT NULL,
	bitActivo BIT NOT NULL
);
CREATE TABLE Division(
	DivisionID INT PRIMARY KEY AUTO_INCREMENT,
    DepartamentoID INT NOT NULL,
    foreign key (DepartamentoID) references Departamento(DepartamentoID),
    vchDescripcion varchar(100) not null,
    bitActivo bit not null
);
CREATE TABLE Empleado(
	EmpleadoID INT PRIMARY KEY AUTO_INCREMENT,
    vchNombre VARCHAR(100) NOT NULL,
    vchExtension VARCHAR(5) NOT NULL,
    vchPuesto VARCHAR(50) NOT NULL,
    vchArea VARCHAR(60) NOT NULL,
    vchCorreo VARCHAR(100) NOT NULL,
    vchCelular VARCHAR(15),
    DepartamentoID INT NOT NULL,
    foreign key (DepartamentoID) references Departamento(DepartamentoID),
    DivisionID INT NOT NULL,
    foreign key (DivisionID) references Division(DivisionID),
    vchNumeroDirecto varchar(100),
    vchFoto VARCHAR(150),
    bitActivo BIT NOT NULL
);
