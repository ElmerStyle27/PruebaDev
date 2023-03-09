const instanceVenta = new Vue({
    el: '#modVenta',
    data: {
        titulo: 'Sucursal',
        listadoProductos: [],
        listadoSucursales: [],
        carrito:[],
        sucursalSelect: parseInt(localStorage.getItem('SucursalId')),
        urlPeticiones: location.origin


    },
    created: async function () {
        await this.ObtenerSucursales();
        // await this.llenadoLocalStorage();
        await this.ObtenerProductos();
    },
    methods: {

        async llenadoLocalStorage() {

            let sucursalId = parseInt(localStorage.getItem('SucursalId'));

            if (sucursalId != null || sucursalId != 0) {
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
        async ObtenerProductos() {
            this.listadoProductos = [];
            // Se define la URL a la que se va a hacer la petición GET
            const url = this.urlPeticiones +`/api/Informacion/Productos/Sucursal/${this.sucursalSelect}`;

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
                    this.listadoProductos = data;
                })
                .catch(error => {
                    // Si hubo algún error en la petición, se muestra el mensaje de error
                    console.error(error);
                });

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

        async EliminarProducto(productoId) {
            //const itemId = 1; // ID del elemento a eliminar
            const response = await fetch(this.urlPeticiones +`/api/Productos/EliminarProducto/${productoId}`, {
                method: 'DELETE'
            });
            if (response.ok) {
                this.Alert(1, 'Se elimino con éxito')
                location.reload();
            } else {
                const error = await response.json();
                // Mostrar mensaje de error al usuario
                alert(`Error al eliminar elemento: ${error.message}`);
            }

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
        },


        async AgregarCarrito(Id, precio) {
            let nuevoProducto = { ProductoId: Id, cantidad: 1, costo: precio, sucursalId:  this.sucursalSelect };
            this.carrito.push(nuevoProducto);
            this.Alert(1, 'El producto se agrego con éxito')
            for (let i = 0; i < this.carrito.length; i++) {
                let productoActual = this.carrito[i];

                // Verificamos si el producto ya existe en el arreglo
                let indiceProductoRepetido = this.carrito.findIndex(p => p.ProductoId === productoActual.ProductoId && p !== productoActual);
                if (indiceProductoRepetido !== -1) {
                    // Si el producto ya existe, sumamos la cantidad correspondiente
                    let productoRepetido = this.carrito[indiceProductoRepetido];
                    productoRepetido.cantidad += productoActual.cantidad;
                    // Eliminamos el producto duplicado del arreglo
                    this.carrito.splice(i, 1);
                    // Decrementamos el contador para no omitir el siguiente elemento
                    i--;
                }
            }

        },

        async ComprarCarrito() {
            if (this.carrito.length == 0) {
                this.Alert(2, 'No hay productos en su carrito, favor de agregar productos')
                return 0;
            }

            fetch(this.urlPeticiones + '/api/Ventas/CrearVenta', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(this.carrito)
            })
                .then(response => response.json())
                .then(data => {
                    // Se utiliza la información recibida de la respuesta
                    let result = data;
                    (result == true) ? this.Alert(1, 'El carrito se compro correctamente') : this.Alert(2, 'Algunos de los productos no cuentan con stock, intente nuevamente');
                    location.reload();
                })
                .catch(error => console.error(error));


        }





    },
    computed: {
    }
})