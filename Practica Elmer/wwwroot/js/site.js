const instanceV = new Vue({
    el: '#modV',
    data: {
        titulo: 'Sucursal',
        listadoProductos: [],
        listadoSucursales: [],
        sucursalSelect: 0
    },
    created: async function () {
        await this.llenadoLocalStorage();

    },
    methods: {

        async llenadoLocalStorage() {

            let sucursalId = parseInt(localStorage.getItem('SucursalId'));

            if (!!sucursalId) {
                this.sucursalSelect = sucursalId;
            }
            else {
                localStorage.setItem('SucursalId', 1);
                let sucursalId = parseInt(localStorage.getItem('SucursalId'));
                this.sucursalSelect = sucursalId;
            }
        }
    },
    computed: {
    }
})