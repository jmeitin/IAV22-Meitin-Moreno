# IAV22-Meitin-Moreno
Proyecto Final IA 2022

Como proyecto final voy a hacer una IA que aprenda a recorrer un circuito sin chocarse con las paredes a base de prueba y error utilizando raycast. La IA enviará varios objetos por el circuito, y en el caso de que colisionen recibirá un feedback negativo. Cuanto más lejos consigan llegar el feedback será mejor, por lo que los nuevos objetos que envie serán condicionados por dichos resultados.

He estado investigando y he visto ciertos plugins de Unity que podrían ser útiles y educativos. [Unity ML-Agents Toolkit](https://github.com/Unity-Technologies/ml-agents?utm_source=Unity+YouTube&utm_medium=social&utm_campaign=evangelism_global_generalpromo_2020-05-20_ml-agents-toolkit#readme)

Dependiendo del alcance podría complicarlo haciendo un laberinto y haciendo que varias IAs intenten resolverlo (cada IA tendría distintos paremetros de penalización/recompensa al chocar/avanzar, pudiendo asi determinar si es más util que se penalice mucho el fracasar, o que la recompensa sea muy grande por avanzar más).
