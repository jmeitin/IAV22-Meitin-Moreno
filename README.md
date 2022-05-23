# Documentación Scared Bird - Proyecto Final IA
*Javier Meitín Moreno*

## Descripción:
Scared Bird es un juego shooter en primera persona para PC donde el jugador toma el rol de un Cazador que debe acabar con todos los pájaros en el bosque. Estos pájaros vuelan en bandada siguiendo al Pájaro Jefe. Cuando oyen un disparo, los pájaros menores huyen del lugar, hasta haberse tranquilizado al cabo de unos segundos. Al pasar cierto intervalo de tiempo vuelven a seguir al pájaro jefe como hacían antes. En caso de que el cazador mate al Pájaro jefe, la bandada se desintegrará y todos los pájaros pasarán a volar de manera aleatoria. El juego acabará cuando el Cazador haya acabado con todos los pájaros. Dado que el merodeo de el pájaro es imprevisible, el jugador podrá atraerlo con un spray verde que puede instanciar un máximo de 3 veces. En cuanto el pájaro lo vea se acercará a investigar.

* **Video demostración:** https://www.youtube.com/watch?v=rZg6bURmuxk&ab_channel=JAVIERMEIT%C3%8DNMORENO

## Controles:
### Jugador:
* **Movimiento:** El jugador podrá controlar el movimiento del jugador mediante las teclas **WASD** para moverse sobre el escenario.
* **Mirar:** El jugador puede cambiar la dirección en la que mira la cámara moviendo el **ratón**.
* **Disparar:** El jugador puede disparar con la tecla **SPACE**.
* **Spray:** El jugador puede instanciar un spray para atreaer al pájaro jefe con la tecla **C**. (Solo en caso de que aún le queden)

## Entidades:
### Cazador:
El cazador es controlado por el jugador, el cual puede realizar unicamente 2 acciones:
* **Movimiento:** El jugador puede mover libremente al cazador por el escenario mediante el teclado.
* **Disparar:** El jugador puede disparar mediante la tecla espaciadora.

### Pajaro:
El comportamiento de los pajaros por los siguientes algoritmos:
* **Merodear:** No es estrictamente un merodeo como el utilizado en las prácticas anteriores, si no un seguimiento a un target invisible que va cambiando a una nueva posición aletaoria cada vez que el pájaro le alcanza.
* **Seguimiento:** Los pájaros menores siguien al pájaro jefe.
* **Huida:** En caso de que un pájaro explote el resto huye en dirección contraria.

## Scripts:
### Player:
*Para hacer el movimento de la cámara en primera persona he seguido un pequeño tutorial de youtube, reciclando lo que me podía ser útil.*

El juego tiene asociado un package de Input System (Version 1.0.2 - January 21, 2021) del Registro de Unity. Aunque el juego se podría desarrollar utilizando el Input que viene incorporado en Unity por defecto, este paquete facilita mucho trabajo y proporciona muchos soportes que facilitarían escalar el proyecto en un futuro.

El principal uso que se le da a este paquete en este proyecto es la de utilizar un **InputActions** llamado **PlayerInput** para gestionar las acciones que puede realizar el jugador, y a que teclas/ratón están asociados. 

![image](https://user-images.githubusercontent.com/62613312/169841977-c6494bde-5687-45bb-acb3-1a0c0a570c57.png)

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

#### PlayerShoot
Se encarga de disparar una bala en la dirección en la que mira la cámara en ese instante. Dichas balas no son entidades físicas. Unicamente se proyecta un **raycast** desde la posición de player, y en caso de chocar con un pájaro, avisa al GameManager.

```js
function Shoot():

   # Proyectar Ray
   ray = Ray(camara.position, camara.vectorForward)
   # comprobar colision ==> En caso de detectar colision, objetivo devuelve una ref a el. Por eso es out.
   if Raycast(ray, out objetivo) then
      if(objetivo is pajaro)
         KillBird
```

#### Input Manager:
Se encarga de generar una instancia del **PlayerInput** para poder enviarle al PlayerMotor y PlayerLook los valores del teclado y ratón en los Update. También detecta la pulsación de la tecla C para instanciar un nuevo spray.

```js
speed: value, with default value of 5

function Update():
   # DISPARAR
   if andando.Disparar.triggered then
      playerShooter.ProcessShoot(disparar)
      
   # LANZAR SPRAY ==> AVISAR AL GM
   if andando.Atraer.triggered then
         if GameManager.SpraysAvailable then
                GameManager.traerPajaro(player.position)
   
function FixedUpdate():
    playerMotor.ProcessMove(andando.Movimiento)
    
function LateUpdate()
    playerLook.ProcessLook(andanado.Mirar)
```
![image](https://user-images.githubusercontent.com/62613312/169839087-7ee2a12f-5505-4275-bc17-7a2901843b1f.png)

#### Diagrama:
![Untitled Diagram drawio (1)](https://user-images.githubusercontent.com/62613312/167623958-ac518a80-2c44-4ccd-a47f-d1d91e2fbabb.png)

### Target:
Una entidad invisible que sirve como destino del NavMeshAgent del pájaro jefe. Tiene un método para cambiar a una nueva posición aleatoria dentro de las dimensiones del área de vuelo. Si choca con un árbol cambia a una nueva posición. 

#### RandomMoveTarget:
```js
# Las dimensiones del area sobre la que puede volar el pájaro.
dimensiones: float, with default value of 30

    unction SetRandomPosition(newPos)
        transform.SetPosition(newPos)
        
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
#### Bird:
Los pájaros tiene aosciados un script que los identifica como tales, guarda su valor en puntos que puede ganar el player si los mata, e instancia una explosión al morir.
```js
soyJefe: bool, default value false
points: value, default value 10

function KillBird:
   Instantiate explosion
   GameManager.BirdDied(points, position, soyJefe)
   Deactivate this gameObject
```
#### BirdNavMesh:
Los pájaross merodean mediante un NavMeshAgent. Hay un objeto vacío en la escena que funciona a modo de target. El pájaro se mueve hasta él evitando los obstáculos en el camnio, y una vez lo alcanza, el target se teletransporta a una nueva posición aletaroria en el mapa. De esta manera vuelve a comenzar el vuelo del pájaro jefe. Tambien hay un método Huida que será llamado cuando muera un pájaro. Este facilitará el cambio de estados entre seguir y huir.
```js
target: Transform
navMeshAgent: NavMeshAgent

function Update():
    navMeshAgent.destination = target.position
    # Tambien cambia aleatoriamente la posicion del target cada cierto INTERVALO de tiempo
    
function OnTriggerEnter(Collider other):
        # El pajaro alcanzo su objetivo
        if other == Target then
            target.randomMoveTarget.SetRandomPosition();
           
#Ha muerto un pajaro disparado
function Huir(Vector3 disparoPos, bool jefeDied):
   dirHuida = currentPos - disparo
   target.SetPosition(dirHuida + currentPos)
   Invoke "SinMiedo" in 5 seconds
    
function SinMiedo:
   # Vuelvo a merodear de manera aleatoria
   if jefeDied || soyJefe then
      target.SetRandomPosition
   # Vuelvo a seguir al jefe y dejo de merodear
   else then
      PajaroSeguir.enable
      this.disable
      
# EL PAJARO TIENE QUE IR HACIA EL SPRAY VERDE
function PlayerCall(Vector3 pos):
    # EL SPRAY SE PONE EN LA ULTIMA POS DE PLAYER
    target.position = pos
    Instantiate spray
```

### Pájaro Menor:
#### PajaroSeguir:
Modifica el navMesh para que se acerque al target, en este caso el Pájaro jefe. No tiene ningún otro método.

#### Bird:
*Descrito anteriormente.*

#### BirdNavMesh:
*Descrito anteriormente.*

### GameManager:
Se encarga de gestionar la puntuación, la escena, y el número de pájaros.

```js
# Variables que llevan la cuenta de los datos de la partida
deadBirds : value, default value 0;
aliveBirds : value, default value Number of birds in the scene;
score : value, default value 0;
shots : value, default value 0;
sprays : value, default value 3;

function BirdDied(int destructionPoints, Vector3 posDeadBird, bool eraJefe):
    # MODIFICAR UI
    aliveBirds--
    deadBirds++
    score += destructionPoints
    update UI
    
    # MODIFICAR COMPORTAMIENTO
    //COMPORTAMIENTO
    for all pajaros
      if pajaro is Jefe then
         BirdNavMesh.Huir(posDeadBird, eraJefe)
         
      else if pajaro is Menor then
         DeActivate PajaroSeguir
         Activate BirdNavMesh
         BirdNavMesh.Huir(posDeadBird, eraJefe)
         
function AtraerPajaro(Vector3 posPlayer):
    # 0 es el jefe
    if pajaros[0].active then
      pajaros[0].BirdNavMesh.PlayerCall(posPlayer)

function SpraysAvailable():
    if spray > 0 && jefeIsAlive then
      spray--
      uiManager update
      return true
        
    return false;
```

#### Ciclo de llamadas al disparar a un pájaro
![GameManagerBird (2)](https://user-images.githubusercontent.com/62613312/169375958-f42bceba-b21a-4320-b36b-0554f10fe0f2.jpg)

### UIManager:
Se encarga de mostrar en pantalla el número de pájaros abatidos y los disparos usados.

![image](https://user-images.githubusercontent.com/62613312/169840937-948d2c8e-9b31-4438-b286-98293835dfe2.png)

## Assets:
Todos los assets usados son gratuitos. O bien los he creado yo, o los he cogido de internet:
* **Árbol:** https://free3d.com/es/modelo-3d/tree-74556.html
* **Material de arena:** https://assetstore.unity.com/packages/2d/textures-materials/floors/yughues-free-sand-materials-12964
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
