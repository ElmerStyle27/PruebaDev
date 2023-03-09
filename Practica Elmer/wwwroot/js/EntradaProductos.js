const instanceEntradaProductos = new Vue({
    el: '#modEntrada',
    data: {
        titulo: 'Sucursal',
        nombre: '',
        codigo: '',
        cantidad: 0,
        precio: 0,
        listadoSucursales: [],
        sucursalSelect: parseInt(localStorage.getItem('SucursalId')),
        urlPeticiones: location.origin
    },
    created: async function () {
        await this.ObtenerSucursales();
    },
    methods: {

        async llenadoLocalStorage() {

            let sucursalId = parseInt(localStorage.getItem('SucursalId'));

            if (sucursalId != null || 0) {
                this.sucursalSelect = sucursalId;
            }
            else {
                localStorage.setItem('SucursalId', 1);
                let sucursalId = parseInt(localStorage.getItem('SucursalId'));
                this.sucursalSelect = sucursalId;
            }


        },
        async CambiarSucursal() {
            localStorage.setItem('SucursalId', this.sucursalSelect);
            location.reload();

        },

        async ObtenerSucursales() {

            // Se define la URL a la que se va a hacer la petición GET
            const url = this.urlPeticiones + '/api/Informacion/Sucursales';

            // Se hace la petición GET utilizando el método fetch de JavaScript
            fetch(url)
                .then(response => {
                    if (response.ok) {


                        // Si la respuesta es exitosa, se convierte la respuesta en formato JSON
                        return response.json();
                    }
                    throw new Error('Error en la petición.');
                })
                .then(data => {
                    // Se utiliza la información recibida de la respuesta
                    console.log(data);
                    this.listadoSucursales = data;
                })
                .catch(error => {
                    // Si hubo algún error en la petición, se muestra el mensaje de error
                    console.error(error);
                });

        },
        async SubirProducto() {

           

            fetch(this.urlPeticiones +'/api/Productos/CrearProducto', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({
                    Nombre: this.nombre,
                    CodigoBarras: this.codigo,
                    Cantidad: this.cantidad,
                    PrecioUnitario: this.precio,
                    SucursalId: this.sucursalSelect
                })
            })
                .then(response => response.json())
                .then(data => {
                    // Se utiliza la información recibida de la respuesta
                    let result = data;
                    (result == true) ?  this.Alert(1, 'Producto Guardado con éxito') :  this.Alert(2, 'Error al guardar');
                    location.reload()
                })
                .catch(error => console.error(error));

            
        },
         async Alert(estatus, alerta) {

             var color = (estatus == 1) ? 'green' : 'red';
             var message = alerta;
             var alertBox = document.createElement('div');
             alertBox.innerHTML = message;
             alertBox.style.backgroundColor = color;
             alertBox.style.color = 'white';
             alertBox.style.padding = '10px';
             alertBox.style.borderRadius = '5px';
             alertBox.style.boxShadow = '0px 0px 10px rgba(0, 0, 0, 0.5)';
             alertBox.style.position = 'fixed';
             alertBox.style.top = '20%';
             alertBox.style.left = '50%';
             alertBox.style.transform = 'translate(-50%, -50%)';
             alertBox.style.width = '300px'; // Ancho del cuadro de alerta
             document.body.appendChild(alertBox);

             setTimeout(function () {
                 alertBox.parentNode.removeChild(alertBox);
             }, 3000); // 3000 milisegundos = 3 segundos
        }
    },
    computed: {
    }
})