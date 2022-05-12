# Documentación Scared Bird - Proyecto Final IA
*Javier Meitín Moreno*

## Descripción:
Scared Bird es un juego shooter en primera persona para PC donde el jugador toma el rol de un Cazador que debe acabar con todos los pájaros en el bosque. Estos pájaros vuelan en bandada siguiendo al Pájaro Jefe. Cuando oyen un disparo, los pájaros menores huyen del lugar, hasta haberse tranquilizado al cabo de unos segundos. Al pasar cierto intervalo de tiempo vuelven a seguir al pájaro jefe como hacían antes. En caso de que el cazador mate al Pájaro jefe, la bandada se desintegrará y todos los pájaros pasarán a volar de manera aleatoria. El juego acabará cuando el Cazador haya acabado con todos los pájaros.

## Entidades:
### Cazador:
El cazador es controlado por el jugador, el cual puede realizar unicamente 2 acciones:
* **Movimiento:** El jugador puede mover libremente al cazador por el escenario mediante el teclado.
* **Disparar:** El jugador puede disparar mediante la tecla espaciadora.

### Pajaro Jefe:
El comportamiento del Pajaro Jefe viene definido por 1 algoritmo.
* **Merodear:** No es estrictamente un merodeo como el utilizado en las prácticas anteriores, si no un seguimiento a un target invisible que va cambiando a una nueva posición aletaoria cada vez que el pájaro le alcanza.

### Pájaro Menor:
* **Seguimiento:** Sigue al Pájaro jefe.
* **Huida:** Si oye un disparo oye en dirección contraria por unos segundos.
* **Merodeo:** En caso de que muera el Pájaro jefe, los pájaros menores pasan a merodear.

## Scripts:
### Player:
*Para hacer el movimento de la cámara en primera persona he seguido un pequeño tutorial de youtube, reciclando lo que me podía ser útil.*

El juego tiene asociado un package de Input System (Version 1.0.2 - January 21, 2021) del Registro de Unity. Aunque el juego se podría desarrollar utilizando el Input que viene incorporado en Unity por defecto, este paquete facilita mucho trabajo y proporciona muchos soportes que facilitarían escalar el proyecto en un futuro.

El principal uso que se le da a este paquete en este proyecto es la de utilizar un **InputActions** llamado **PlayerInput** para gestionar las acciones que puede realizar el jugador, y a que teclas/ratón están asociados. 

![image](https://user-images.githubusercontent.com/62613312/167615459-9c44360d-e5bd-4a9e-bd6d-66598a95a755.png)

Una vez definidas las asociaciones entre input y acciones se genera un Script C# también llamado **PlayerInput** que puede ser consultado por otros Scripts con el fin de determinar que tecla se ha pulsado en ese frame.

El Player tiene asociado un componente **CharacterController** con el fin modificar su movimiento mediante los siguientes scripts:

#### Player Motor:
Se encarga de modificar el componente **CharacterController** del player en función de los valores de las teclas WASD proporcionados por el InputManager (PlayerInput).
```js
speed: value, with default value of 5

function ProcessMove(Vector2 WASD):

   characterController.Move(transform->dir(WASD) * speed * deltaTime)
```

#### Player Look:
Se encarga de modificar la rotación de la **Main Camera**, la cual es hija de Player, y la del transform del propio Player, en función del input del ratón proporcionados por el InputManager (PlayerInput).
```js
camara: Camera 
xRotation: value, with default value of 0
sensitivity: value, with default value of 0

function ProcessLook(Vector2 mouse):

   # Calcular la rotación
   xRotation -= mouse.y * deltaTime * sensitivity
   # Aplicar la rotación a la camara
   camara->transform->localRotation = Eulriano(xRotation,0,0)
   # Rotar player
   transform.Rotate(Vector3.up * mouseX * deltaTime * sensitivity
```

#### PlayerShooter
Se encarga de disparar una bala en la dirección en la que mira la cámara en ese instante. Dichas balas no son entidades físicas. Unicamente se proyecta un raycast desde la posición de player, y en caso de chocar con un pájaro, avisa al GameManager.

#### Input Manager:
Se encarga de generar una instancia del **PlayerInput** para poder enviarle al PlayerMotor y PlayerLook los valores del teclado y ratón en los Update. 
```js
speed: value, with default value of 5

function FixedUpdate():
    playerMotor.ProcessMove(andando.Movimiento)
    playerShooter.ProcessShoot(disparar)
    
function LateUpdate()
    playerLook.ProcessLook(andanado.Mirar)
```

#### Diagrama:
![Untitled Diagram drawio (1)](https://user-images.githubusercontent.com/62613312/167623958-ac518a80-2c44-4ccd-a47f-d1d91e2fbabb.png)

### Target:
Una entidad invisible que sirve como destino del NavMeshAgent del pájaro jefe. Tiene un método para cambiar a una nueva posición aleatoria dentro de las dimensiones del área de vuelo. Si choca con un árbol cambia a una nueva posición. 

#### RandomMoveTarget:
```js
# Las dimensiones del area sobre la que puede volar el pájaro.
dimensiones: float, with default value of 30

    function SetRandomPosition()
        x = Random.Range(-dimensiones, dimensiones)
        y = position.y
        z = Random.Range(-dimensiones, dimensiones)
        transform.SetPosition(x, y, z)

     function OnTriggerStay(Collider other)
        if other == Tree then
            SetRandomPosition()
```

### Pajaro Jefe:
#### Merodear:
Merodea mediante un NavMeshAgent. Hay un objeto vacío en la escena que funciona a modo de target. El pájaro se mueve hasta él evitando los obstáculos en el camnio, y una vez lo alcanza, el target se teletransporta a una nueva posición aletaroria en el mapa. De esta manera vuelve a comenzar el vuelo del pájaro jefe.
```js
target: Transform
navMeshAgent: NavMeshAgent

function Update():
    navMeshAgent.destination = target.position
    
function OnTriggerEnter(Collider other)
        # El pajaro alcanzo su objetivo
        if other == Target then
            target.randomMoveTarget.SetRandomPosition();
```

### Pajaro Menor:
#### Seguimiento:
El seguimiento funciona al igual que el Merodeo salvo que el target es el pájaro jefe.

#### Huida:
En caso de que se produzca un disparo, los pájaros en proximidad huyen en dirección contraria.

### GameManager:
Se encarga de gestionar la puntuación, la escena, y el número de pájaros.

```js
# Variables que llevan la cuenta de los datos de la partida
deadBirds : value, default value 0;
aliveBirds : value, default value Number of birds in the scene;
score : value, default value 0;
shots : value, default value 0;;

function BirdDied(int destructionPoints):
    aliveBirds--
    deadBirds++
    score += destructionPoints
    update UI
```

### UIManager:
Se encarga de mostrar en pantalla el número de pájaros abatidos y los disparos usados.
![image](https://user-images.githubusercontent.com/62613312/168070359-efb42d1a-a4f7-487b-a4b9-90b47d9130a1.png)

## Controles:
### Jugador:
* **Movimiento:** El jugador podrá controlar el movimiento del jugador mediante las flechas/teclas WASD para moverse sobre el escenario.
* **Mirar:** El jugador puede cambiar la dirección en la que mira la cámara moviendo el ratón.
* **Disparar:** El jugador puede disparar con la tecla espaciadora.

## Assets:
Todos los assets usados son gratuitos. O bien los he creado yo, o los he cogido de internet:
* **Árbol:** https://free3d.com/es/modelo-3d/tree-74556.html
* **Árboles:** https://free3d.com/es/modelo-3d/trees-9-53338.html
* **Pájaro:** https://free3d.com/es/modelo-3d/gull-17126.html
* **Skybox:** https://assetstore.unity.com/packages/2d/textures-materials/sky/free-hdr-skyboxes-pack-175525

## Referencias:
* Unity 2018 Artificial Intelligence Cookbook, Second Edition (Repositorio)
https://github.com/PacktPublishing/Unity-2018-Artificial-Intelligence-Cookbook-Second-Edition
* Unity Artificial Intelligence Programming, Fourth Edition (Repositorio)
https://github.com/PacktPublishing/Unity-Artificial-Intelligence-Programming-FourthEdition
* Unity Documentation:
https://docs.unity.com/
* #1 FPS Movement: Let's Make a First Person Game in Unity!
https://www.youtube.com/watch?v=rJqP5EesxLk&ab_channel=NattyCreations
