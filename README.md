Proyecto en arquitectura limpia

Es un proyecto que implementa una arquitectura limpia utilizando CQRS, MediatR, Mapper, Unit of Work, Repository pattern y .Net Core 6.

El objetivo del proyecto es organizar el código de forma que sea fácil de mantener, reutilizar y adaptar a diferentes tecnologías. Con clean architecture, se separan las responsabilidades de cada capa del software, 
se siguen los principios SOLID y se enfoca en el dominio del problema y no en los detalles de implementación. 
Así, se logra un software que es independiente del framework, la interfaz de usuario, la base de datos y cualquier otro elemento externo.

Dentro del controllador tenemos la posibilidad de gestionar permisos de usuarios, el mismo ofrece tres servicios:

  * Request permission
  
  * Modify permission
  
  * Get permission
  
Cada ejecución de servicio o end point:

* Crea un registro en Serilog
  
* Indexa metadata de la solicitud a ElasticSearch (cuando la solicitud es Request Permission y Modify Permission)

* Cuando la solicitud es Get Permission, obtiene todos los registro de ElasticSearch (*) y realiza un Mappeo para devolver una lista de PermissionVM.
  
* Tambien trabaja con Apache Kafka en un entorno local.

    * Se crea un Topic con nombre de Operation
  
    * Persiste los datos como Producer por cada solicitud con la siguiente estructura.
    
      * Guid( Guid autogenerado)
        
      * Description (Nombre de la operacion que se ejecuta).

    * Para realizar las pruebas como consumer puedes ejecutar el siguiente instruccion en la terminal: bin/kafka-console-consumer.sh --topic operation --from-beginning --bootstrap-server localhost:9092
        
* Además, el proyecto incluye un proyecto para Test utilizando Mock y XUnit.
* Finalmente en proyecto se encuentra un archivo de DockerFile y también se cuenta con el docker-compose de ElasticSearch y Kibana.

Instalación:

Para instalar el proyecto, sigue estos pasos:

* Clona el repositorio

* Cambia tu cadena de conexion en AppSetting
  
* Compila el proyecto para que se instalen las dependencias

* Debes de ejecutar el docker-compose de ElasticSearch
  
* Se debe instalar en un entorno local o docker de apache kafka
  
* Tambien puedes crear una imagen de docker con el dockerfile y proceder a crear el contenedor correspodiente.

Si quieres contribuir al proyecto, puedes hacerlo de la siguiente manera:

* Crea una rama con tu nombre o el nombre de la funcionalidad que quieres añadir: git checkout -b <nombre-rama>

* Haz los cambios que consideres necesarios y haz commit de ellos: git commit -am "Mensaje del commit"

* Sube tu rama al repositorio remoto: git push origin <nombre-rama>

* Crea un pull request desde tu rama a la rama principal y espera a que sea revisado y aprobado.
