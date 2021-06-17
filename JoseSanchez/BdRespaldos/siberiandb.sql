/*
 Navicat Premium Data Transfer

 Source Server         : laragon
 Source Server Type    : MySQL
 Source Server Version : 50724
 Source Host           : localhost:3306
 Source Schema         : siberiandb

 Target Server Type    : MySQL
 Target Server Version : 50724
 File Encoding         : 65001

 Date: 16/06/2021 21:51:58
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for ciudad
-- ----------------------------
DROP TABLE IF EXISTS `ciudad`;
CREATE TABLE `ciudad`  (
  `IDCiudad` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT,
  `NombreCiudad` varchar(150) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `FechaCreacion` timestamp(0) NULL DEFAULT NULL,
  PRIMARY KEY (`IDCiudad`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 9 CHARACTER SET = utf8 COLLATE = utf8_unicode_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of ciudad
-- ----------------------------
INSERT INTO `ciudad` VALUES (6, 'CALI', '2021-06-16 21:05:24');
INSERT INTO `ciudad` VALUES (7, 'MANTA', '2021-06-16 21:05:50');

-- ----------------------------
-- Table structure for restaurantes
-- ----------------------------
DROP TABLE IF EXISTS `restaurantes`;
CREATE TABLE `restaurantes`  (
  `IDRestaurante` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT,
  `NombreRestaurante` varchar(150) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `IDCiudad` bigint(20) UNSIGNED NOT NULL,
  `NumeroAforo` bigint(20) NULL DEFAULT NULL,
  `Telefono` varchar(150) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL,
  `FechaCreacion` timestamp(0) NULL DEFAULT NULL,
  PRIMARY KEY (`IDRestaurante`) USING BTREE,
  INDEX `IDCiudad`(`IDCiudad`) USING BTREE,
  CONSTRAINT `restaurantes_ibfk_1` FOREIGN KEY (`IDCiudad`) REFERENCES `ciudad` (`IDCiudad`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE = InnoDB AUTO_INCREMENT = 20 CHARACTER SET = utf8 COLLATE = utf8_unicode_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of restaurantes
-- ----------------------------
INSERT INTO `restaurantes` VALUES (16, 'panaderia', 7, 30, '345345353', '2021-06-16 21:39:45');
INSERT INTO `restaurantes` VALUES (17, 'AGUA', 7, 65, '345345353', '2021-06-16 21:40:13');
INSERT INTO `restaurantes` VALUES (18, 'EEUU', 6, 23, '345345353', '2021-06-16 21:40:33');
INSERT INTO `restaurantes` VALUES (19, 'parker', 7, 77, '345345353', '2021-06-16 21:40:49');

-- ----------------------------
-- Procedure structure for Sp_Ciudades
-- ----------------------------
DROP PROCEDURE IF EXISTS `Sp_Ciudades`;
delimiter ;;
CREATE PROCEDURE `Sp_Ciudades`(IN opcion text,
                        IN _IDCiudad int,
                        IN _NombreCiudad varchar(150) ,
                        OUT out_cod INT,
                        OUT out_msj VARCHAR(500))
BEGIN

declare _valida int;
declare _valida2 int;
case opcion  

	## Lista de Ciudades
  when 'I' then 
        SELECT 
		IDCiudad,
		NombreCiudad,
		FechaCreacion
		FROM ciudad ;
		
		SET out_cod = 7;
		SET out_msj = 'Lista Ciudades';
		SELECT out_cod, out_msj;
	
	## Ciudad por nombre
  when 'II' then
		 SELECT 
		IDCiudad,
		NombreCiudad,
		FechaCreacion
		FROM ciudad
        WHERE NombreCiudad LIKE concat('%',_NombreCiudad,'%') ;
        
		SET out_cod = 7;
		SET out_msj = 'Ciudades buscar por nombre';
		SELECT out_cod, out_msj;
        
	## Insert
  when 'III' then 
	
    set _valida = (SELECT count(*) FROM ciudad WHERE NombreCiudad LIKE concat('%',_NombreCiudad,'%') );
		
		IF _valida>0 THEN
				SET out_cod = 6;
				SET out_msj =  'NOMBRE DE CIUDAD YA EXISTE';
				SELECT out_cod, out_msj;
		ELSE
			 INSERT INTO ciudad
		(
		NombreCiudad,FechaCreacion)
		VALUES
		(_NombreCiudad,NOW());
		
		SET out_cod = 7;
		SET out_msj = 'SE HA CREADO CORRECTAMENTE CIUDAD';
		SELECT out_cod, out_msj;
            
     
         

		END IF;
       
	
    ##Update
	when 'IV' then 
	
    set _valida = (SELECT count(*) FROM ciudad WHERE IDCiudad = _IDCiudad );
		IF _valida=0 THEN
				SET out_cod = 6;
				SET out_msj =  'No Existe el IDCiudad -> Ciudad... no se puede ACTUALIZAR!';
				SELECT out_cod, out_msj;
		ELSE
			UPDATE ciudad
			set NombreCiudad = _NombreCiudad
			where IDCiudad = _IDCiudad ;
			
			SET out_cod = 7;
			SET out_msj = 'SE HA MODIFICADO CIUDAD CORRECTAMENTE';
			SELECT out_cod, out_msj;
            
		END IF;
        
	 ##DELETE
	when 'V' then 
	
		set _valida = (SELECT count(*) FROM ciudad WHERE IDCiudad = _IDCiudad );
		
		set _valida2 = (SELECT count(*) FROM restaurantes WHERE IDCiudad = _IDCiudad );
	
		IF _valida=0 THEN
				SET out_cod = 6;
				SET out_msj =  'No Existe el Id -> Ciudad... no se puede borrar!';
				SELECT out_cod, out_msj;
		ELSEIF _valida2>0 THEN
				SET out_cod = _valida;
				SET out_msj =  'NO SE PUEDE BORRAR PORQUE TIENE UN RESTAURANTE ASOCIADO';
				SELECT out_cod, out_msj;
		ELSE
        DELETE FROM ciudad WHERE  IDCiudad = _IDCiudad ;
			 SET out_cod = _valida;
			 SET out_msj = 'SE HA BORRADO CIUDAD CORRECTAMENTE';
			 SELECT out_cod, out_msj;
		END IF;
			
		ELSE BEGIN END;
end case;

END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for Sp_Restauranes
-- ----------------------------
DROP PROCEDURE IF EXISTS `Sp_Restauranes`;
delimiter ;;
CREATE PROCEDURE `Sp_Restauranes`(IN opcion text,
                        IN _IDRestaurante int,
                        IN _NombreRestaurante varchar(150) ,
                        IN _IDCiudad int,
                        IN NumeroAforo int,
                        IN Telefono varchar(150) ,
						OUT out_cod INT,
                        OUT out_msj VARCHAR(500))
BEGIN

declare _valida int;
declare _valida2 int;
case opcion  

	## Lista de Restaurantes con el nombre de la ciudad
  when 'I' then 
	
    	

        SELECT 
		a.IDRestaurante,
		a.NombreRestaurante,a.IDCiudad,a.NumeroAforo,a.Telefono,a.FechaCreacion,
		b.NombreCiudad
		FROM Restaurantes a INNER JOIN Ciudad b on a.IDCiudad = b.IDCiudad;
		
		SET out_cod = 7;
		SET out_msj = 'Lista restaurantes';
		SELECT out_cod, out_msj;
	
## Filtro por 1 solo restaurante
  when 'II' then 
	
    
        SELECT 
		a.IDRestaurante,
		a.NombreRestaurante,a.IDCiudad,a.NumeroAforo,a.Telefono,a.FechaCreacion,
		b.NombreCiudad
		FROM Restaurantes a INNER JOIN Ciudad b on a.IDCiudad = b.IDCiudad
        WHERE a.NombreRestaurante 
				LIKE concat('%',_NombreRestaurante,'%') ;
		
		SET out_cod = 7;
		SET out_msj = 'Filtro por 1 solo restaurante';
		SELECT out_cod, out_msj;
        
	## Insert
  when 'III' then 
	
    set _valida2 = (SELECT count(*) FROM ciudad where IDCiudad = _IDCiudad );
		set _valida = (SELECT count(*) FROM restaurantes WHERE NombreRestaurante LIKE concat('%',_NombreRestaurante,'%') );
		IF _valida2=0 THEN
				SET out_cod = 7 ;
				SET out_msj =  'No Existe el Id -> Cuidad... no se puede Insertar!';
				SELECT out_cod, out_msj;
				
		
		ELSEIF _valida>0 THEN
				SET out_cod = _valida;
				SET out_msj =  'NOMBRE DE Restaurante YA EXISTE';
				SELECT out_cod, out_msj;
		ELSE
			 INSERT INTO restaurantes
		(
		NombreRestaurante,IDCiudad,NumeroAforo,Telefono,FechaCreacion)
		VALUES
		(_NombreRestaurante,_IDCiudad,NumeroAforo,Telefono,NOW());
		
		SET out_cod = 7;
		SET out_msj = 'SE HA CREADO CORRECTAMENTE';
		SELECT out_cod, out_msj;
            
     
         

		END IF;
       
	
    ##Update
	when 'IV' then 
	
    set _valida = (SELECT count(*) FROM restaurantes where  IDRestaurante = _IDRestaurante );
	set _valida2 = (SELECT count(*) FROM ciudad where IDCiudad = _IDCiudad );
		IF _valida=0 THEN
				SET out_cod = 6;
				SET out_msj =  'No Existe el Id -> Restaurante... no se puede ACTUALIZAR!';
				SELECT out_cod, out_msj;
		ELSEIF _valida2=0 THEN
				SET out_cod = 6;
				SET out_msj =  'No Existe el Id -> Cuidad... no se puede ACTUALIZAR!';
				SELECT out_cod, out_msj;
		ELSE
			UPDATE restaurantes
			set NombreRestaurante = _NombreRestaurante,
			IDCiudad = IDCiudad,
			NumeroAforo = NumeroAforo,
			Telefono = Telefono
			where IDRestaurante = _IDRestaurante ;
			
			SET out_cod = 7;
			SET out_msj = 'SE HA MOOIFICADO CORRECTAMENTE';
			SELECT out_cod, out_msj;
            
		END IF;
        
	 ##DELETE
	when 'V' then 
	
		set _valida = (SELECT count(*) FROM restaurantes where  IDRestaurante = _IDRestaurante );
	
		IF _valida=0 THEN
				SET out_cod = 6;
				SET out_msj =  'No Existe el Id -> Restaurante... no se puede borrar!';
				SELECT out_cod, out_msj;
		ELSE
        DELETE FROM restaurantes WHERE IDRestaurante=_IDRestaurante;
			 SET out_cod = _valida;
			 SET out_msj = 'SE HA BORRADO CORRECTAMENTE';
			 SELECT out_cod, out_msj;
		END IF;
			
		ELSE BEGIN END;
end case;

END
;;
delimiter ;

SET FOREIGN_KEY_CHECKS = 1;
