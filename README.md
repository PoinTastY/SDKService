# SDKService

Este es un servicio de windows que escucha solicitudes mediante un servidor TCP ejecutado sobre un hilo para una escucha constante, el cual recibe y procesa solicitudes para el <a href="https://github.com/PoinTastY/SDKService/blob/master/Models/SDK.cs">SDK de Contpaqi</a>, para garantizar la integridad del sistema Comercial.
Esta implementacion nos permite exponer una API para evitar problemas de contabilidad del SDK, ya que este corre en 32 bits, dejando la posibilidad de implementarlo con sistemas construidos en frameworks mas modernos, e incluso con cualquier lenguaje de programacion, ya escucha y responde con JSON.
