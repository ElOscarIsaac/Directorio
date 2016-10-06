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
INSERT INTO Usuarios(vchNombreEmpleado, vchNombreUsuario, vchPassword, vchCorreoElectronico, datFechaRegistro, bitActivo) VALUES(
	'JESUS ELIAS VAZQUEZ CRUZ', 'jvazquez', 'jomi', 'jvazquez@fujifilm.com.mx', CURDATE(), 1
);
CREATE TABLE Ubicacion(
	UbicacionID INT PRIMARY KEY AUTO_INCREMENT,
    vchDescripcion VARCHAR(100) NOT NULL,
    bitActivo BIT NOT NULL
);
INSERT INTO Ubicacion(vchDescripcion, bitActivo) VALUES ('PISO 6', 1);
INSERT INTO Ubicacion(vchDescripcion, bitActivo) VALUES ('PISO 7', 1);
INSERT INTO Ubicacion(vchDescripcion, bitActivo) VALUES ('PISO 8', 1);
INSERT INTO Ubicacion(vchDescripcion, bitActivo) VALUES ('PISO 9', 1);
INSERT INTO Ubicacion(vchDescripcion, bitActivo) VALUES ('CEDIS', 1);
CREATE TABLE Departamento(
	DepartamentoID INT PRIMARY KEY AUTO_INCREMENT,
    vchDescripcion VARCHAR(100) NOT NULL,
	bitActivo BIT NOT NULL
);
INSERT INTO Departamento(vchDescripcion, bitActivo) VALUES ('INGENIERÍA',1);
INSERT INTO Departamento(vchDescripcion, bitActivo) VALUES ('IMAGING',1);
INSERT INTO Departamento(vchDescripcion, bitActivo) VALUES ('ARTES GRÁFICAS',1);
INSERT INTO Departamento(vchDescripcion, bitActivo) VALUES ('SISTEMAS MÉDICOS',1);
INSERT INTO Departamento(vchDescripcion, bitActivo) VALUES ('PRESIDENCIA',1);
INSERT INTO Departamento(vchDescripcion, bitActivo) VALUES ('VP COMERCIAL',1);
INSERT INTO Departamento(vchDescripcion, bitActivo) VALUES ('VP ADMINISTRACIÓN Y FINANZAS',1);
INSERT INTO Departamento(vchDescripcion, bitActivo) VALUES ('TIENDAS PROPIAS',1);
INSERT INTO Departamento(vchDescripcion, bitActivo) VALUES ('MERCADOTECNIA',1);
INSERT INTO Departamento(vchDescripcion, bitActivo) VALUES ('OPERACIONES',1);
INSERT INTO Departamento(vchDescripcion, bitActivo) VALUES ('TRÁFICO',1);
INSERT INTO Departamento(vchDescripcion, bitActivo) VALUES ('ADMINISTRACIÓN Y FINANZAS',1);
INSERT INTO Departamento(vchDescripcion, bitActivo) VALUES ('SISTEMAS',1);
INSERT INTO Departamento(vchDescripcion, bitActivo) VALUES ('RECURSOS HUMANOS',1);
INSERT INTO Departamento(vchDescripcion, bitActivo) VALUES ('TESORERÍA',1);
INSERT INTO Departamento(vchDescripcion, bitActivo) VALUES ('MEJORA CONTINUA & ASUNTOS REGULATORIOS',1);
INSERT INTO Departamento(vchDescripcion, bitActivo) VALUES ('LABORATORIO CENTRAL',1); 
CREATE TABLE Division(
	DivisionID INT PRIMARY KEY AUTO_INCREMENT,
    DepartamentoID INT NOT NULL,
    foreign key (DepartamentoID) references Departamento(DepartamentoID),
    vchDescripcion varchar(100) not null,
    bitActivo bit not null
);
INSERT INTO Division(DepartamentoID, vchDescripcion, bitActivo) VALUES (1, 'SISTEMAS MÉDICOS',1);
INSERT INTO Division(DepartamentoID, vchDescripcion, bitActivo) VALUES (1, 'ENDOSCOPÍA',1);
INSERT INTO Division(DepartamentoID, vchDescripcion, bitActivo) VALUES (1, 'APLICACIONES',1);
INSERT INTO Division(DepartamentoID, vchDescripcion, bitActivo) VALUES (1, 'FOTOGRAFÍA ',1);
INSERT INTO Division(DepartamentoID, vchDescripcion, bitActivo) VALUES (1, 'IMÁGENES MÉDICAS',1);
INSERT INTO Division(DepartamentoID, vchDescripcion, bitActivo) VALUES (1, 'DESARROLLO ',1);
INSERT INTO Division(DepartamentoID, vchDescripcion, bitActivo) VALUES (2, 'RETAIL',1);
INSERT INTO Division(DepartamentoID, vchDescripcion, bitActivo) VALUES (2, 'FOTOACABADO',1);
INSERT INTO Division(DepartamentoID, vchDescripcion, bitActivo) VALUES (2, 'FOTOGRAFÍA ',1);
INSERT INTO Division(DepartamentoID, vchDescripcion, bitActivo) VALUES (2, 'TELEFONÍA',1);
INSERT INTO Division(DepartamentoID, vchDescripcion, bitActivo) VALUES (3, 'GRAN FORMATO',1);
INSERT INTO Division(DepartamentoID, vchDescripcion, bitActivo) VALUES (3, 'FLEXOGRAFÍA',1);
INSERT INTO Division(DepartamentoID, vchDescripcion, bitActivo) VALUES (3, 'INGENIERIA DE SERVICIO',1);
INSERT INTO Division(DepartamentoID, vchDescripcion, bitActivo) VALUES (4, 'ENDOSCOPIA',1);
INSERT INTO Division(DepartamentoID, vchDescripcion, bitActivo) VALUES (4, 'ULTRASONIDO',1);
INSERT INTO Division(DepartamentoID, vchDescripcion, bitActivo) VALUES (4, 'IVD',1);
INSERT INTO Division(DepartamentoID, vchDescripcion, bitActivo) VALUES (4, 'INFORMATICA MÉDICA',1);
INSERT INTO Division(DepartamentoID, vchDescripcion, bitActivo) VALUES (5, 'NINGUNO',1);
INSERT INTO Division(DepartamentoID, vchDescripcion, bitActivo) VALUES (6, 'NINGUNO',1);
INSERT INTO Division(DepartamentoID, vchDescripcion, bitActivo) VALUES (7, 'NINGUNO',1);
INSERT INTO Division(DepartamentoID, vchDescripcion, bitActivo) VALUES (8, 'NINGUNO',1);
INSERT INTO Division(DepartamentoID, vchDescripcion, bitActivo) VALUES (9, 'NINGUNO',1);
INSERT INTO Division(DepartamentoID, vchDescripcion, bitActivo) VALUES (10, 'NINGUNO',1);
INSERT INTO Division(DepartamentoID, vchDescripcion, bitActivo) VALUES (11, 'NINGUNO',1);
INSERT INTO Division(DepartamentoID, vchDescripcion, bitActivo) VALUES (12, 'NINGUNO',1);
INSERT INTO Division(DepartamentoID, vchDescripcion, bitActivo) VALUES (13, 'NINGUNO',1);
INSERT INTO Division(DepartamentoID, vchDescripcion, bitActivo) VALUES (14, 'NINGUNO',1);
INSERT INTO Division(DepartamentoID, vchDescripcion, bitActivo) VALUES (15, 'NINGUNO',1);
INSERT INTO Division(DepartamentoID, vchDescripcion, bitActivo) VALUES (16, 'NINGUNO',1);
INSERT INTO Division(DepartamentoID, vchDescripcion, bitActivo) VALUES (17, 'NINGUNO',1);
INSERT INTO Division(DepartamentoID, vchDescripcion, bitActivo) VALUES (12, 'CONTABILIDAD FINANCIERA',1);
INSERT INTO Division(DepartamentoID, vchDescripcion, bitActivo) VALUES (12, 'CRÉDITO Y COBRANZA',1);
INSERT INTO Division(DepartamentoID, vchDescripcion, bitActivo) VALUES (12, 'JURÍDICO',1);
INSERT INTO Division(DepartamentoID, vchDescripcion, bitActivo) VALUES (12, 'CONTRALORÍA',1);
INSERT INTO Division(DepartamentoID, vchDescripcion, bitActivo) VALUES (1, 'ADMINISTRACIÓN',1);
INSERT INTO Division(DepartamentoID, vchDescripcion, bitActivo) VALUES (1, 'CALL CENTER',1);
-- use Directorio; drop table Empleado;
CREATE TABLE Empleado(
	EmpleadoID INT PRIMARY KEY AUTO_INCREMENT,
    vchNombre VARCHAR(100) NOT NULL,
    vchExtension VARCHAR(5) NOT NULL,
    vchPuesto VARCHAR(50) NOT NULL,
    vchCorreo VARCHAR(100),
    vchCelular VARCHAR(15),
    DepartamentoID INT NOT NULL,
    foreign key (DepartamentoID) references Departamento(DepartamentoID),
    DivisionID INT NOT NULL,
    foreign key (DivisionID) references Division(DivisionID),
    UbicacionID INT NOT NULL,
    foreign key (UbicacionID) references Ubicacion(UbicacionID),
    vchNumeroDirecto varchar(100),
    txtFoto LONGTEXT,
    bitActivo BIT NOT NULL,
    AUDUSUARIO INT NOT NULL,
    foreign key (AUDUSUARIO) references Usuarios(UsuarioID)
);