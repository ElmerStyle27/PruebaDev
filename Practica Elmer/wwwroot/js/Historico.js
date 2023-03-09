const instanceHistorico = new Vue({
    el: '#modHistorico',
    data: {
        titulo: 'Sucursal',
        listadoHistorico: [],
        listadoSucursales: [],
        sucursalSelect: parseInt(localStorage.getItem('SucursalId')),
        urlPeticiones: location.origin
        
    },
    created: async function () {
        await this.ObtenerSucursales();
        await this.ObtenerHistorico();

    },
    methods: {
       
     
        async CambiarSucursal() {
            localStorage.setItem('SucursalId', this.sucursalSelect);
            location.reload();

        },


        async ObtenerHistorico() {



            // Se define la URL a la que se va a hacer la petición GET
            const url = this.urlPeticiones +`/api/Informacion/HistoricoVentas/${this.sucursalSelect}`;

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
                    this.listadoHistorico = data;
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

        }
    },
    computed: {
    }
})