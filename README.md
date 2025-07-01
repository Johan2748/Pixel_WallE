# Pixel_WallE
Segundo Proyecto de Programación

# Breve Descripción
Pequeño intérprete que tranforma comandos en obras de arte en un canvas, el límite está en tu imaginación

# Clases Principales

_lexer_ : Se encarga de transformar el texto plane del editor de texto en tokens 
_AST_ : Jerarquía de clases q conforman la estructuta principal del programa (expresiones, declaraciones, asignaciones e intrucciones) y verifican su correcta semántica
_Parser_ : Se encarga de tranformar la lista de tokens del lexer en estructuras del AST que le dan forman al programa
_Interpreter_ : Se encarga de evaluar las expresiones y declaraciones del programa, tranformando las partes claves del AST en expresiones y comandos de c#
_Functions_ : Conjunto de funciones válidas del intérprete que se ejecutan sobre el canvas o devuelven un valor
_ErrorManager_ : Se encarga de recoger todos los errores del código y advertir al usuario en caso de que su código sea inválido
_RunCode_ : Se encarga de ejecutar todas las partes esenciales del intérprete y en caso de no haber errores realizar el dibujo correspondiente
_Canvas_ : Contiene toda la parte visual del proyecto y es la q se encarga de mantener y actualizar el estado del canvas 