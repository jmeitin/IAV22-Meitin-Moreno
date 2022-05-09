# Documentación Proyecto Final IA
*Javier Meitín Moreno*

## IDEA PRINCIPAL:
Quiero hacer una bandada de pajaros que se mueve de manera aleatoria evitando obstaculos.

1) pajaro jefe que se mueve aleatoriamente por el entorno evitando obstaculos
2) el resto de los pajaros le siguen pero con un poco de aleatoreidad (no van todos en linea india)
3) el player puede disparar a los pajaros. si acierta este muere y mejora su puntuacion.
4) los pajaros en cierto rango se asustan y abandonan la formacion. al cabo de un rato se reagrupan

## Descripción:
La 

## Punto de Partida: 
### Clases:
#### Player:
Controla el movimiento del jugador y dispara.

#### Bullet:
Mueve el GameObject asociado en la dirección en la que miraba la cámara en ese instante. Si choca con un pájaro lo mata y aumenta la puntuación.

#### GameManager:
Se encarga de gestionar la puntuación, la escena, y el número de pájaros.

## Personajes:
### Cazador:
El cazador es controlado por el jugador, el cual puede realizar unicamente 2 acciones:
* **Movimiento:** El jugador puede mover libremente a Teseo por el escenario mediante el teclado.
* **Disparar:** El jugador puede activar el hilo de Ariadna a voluntad propia. En dicho caso, Teseo seguirá el recorrido que describe hasta llegar a la salida

### Pajaro Jefe:
El comportamiento del Pajaro Jefe viene definido por 3 algoritmos.
* **Merodear:** El Minotauro se mueve de manera aleatoria por el escenario.

### Pajaro Menor:
* **Segumineto:**

## Algoritmos a desarrollar:

### Raycast

## UML:


## Controles:
### Jugador:
* **Movimiento:** El jugador podrá controlar el movimiento del jugador mediante las flechas/teclas WASD para moverse sobre el escenario.
* **Disparar:** El jugador puede disparar con la tecla espaciadora.

## Referencias:
* Unity 2018 Artificial Intelligence Cookbook, Second Edition (Repositorio)
https://github.com/PacktPublishing/Unity-2018-Artificial-Intelligence-Cookbook-Second-Edition
* Unity Artificial Intelligence Programming, Fourth Edition (Repositorio)
https://github.com/PacktPublishing/Unity-Artificial-Intelligence-Programming-FourthEdition
* Unity Documentation:
https://docs.unity.com/

## IDEA ORIGINAL:
Como proyecto final voy a hacer una IA que aprenda a recorrer un circuito sin chocarse con las paredes a base de prueba y error utilizando raycast. La IA enviará varios objetos por el circuito, y en el caso de que colisionen recibirá un feedback negativo. Cuanto más lejos consigan llegar el feedback será mejor, por lo que los nuevos objetos que envie serán condicionados por dichos resultados.

He estado investigando y he visto ciertos plugins de Unity que podrían ser útiles y educativos. [Unity ML-Agents Toolkit](https://github.com/Unity-Technologies/ml-agents?utm_source=Unity+YouTube&utm_medium=social&utm_campaign=evangelism_global_generalpromo_2020-05-20_ml-agents-toolkit#readme)

Dependiendo del alcance podría complicarlo haciendo un laberinto y haciendo que varias IAs intenten resolverlo (cada IA tendría distintos paremetros de penalización/recompensa al chocar/avanzar, pudiendo asi determinar si es más util que se penalice mucho el fracasar, o que la recompensa sea muy grande por avanzar más).
