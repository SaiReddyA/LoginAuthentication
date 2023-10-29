<template>
    <main>
        <h1>Employee Page showing the Employees</h1>

        <div v-if="this.Employees.length ==1">
            <div class="container mt-5">
                <div class="card">
                    <div class="card-header">
                        <h1>Edit Gender</h1>
                        <button style="float: right;" @click="getEmployee()" class="btn btn-primary" >Back to list</button>
                    </div>
                    <form>
                        <div class="card-body">

                            <div class="mb-3">
                                <lable>
                                   Gender Id
                                </lable>
                                <input type="text" class="form-control" v-model="Id" name="gname" id="gname" />
                            </div>
                            <div class="mb-3">
                                <lable>
                                    GenderName
                                </lable>
                                <input type="text" class="form-control" v-model="gender" name="gname" id="gname" />
                            </div>
                            <div class="mb-3">
                                <lable>
                                    Code
                                </lable>
                                <input type="text" class="form-control" v-model="code" name="code" id="code" />
                            </div>
                            <div>
                                <buttton @click="UpdateGender()" class="btn btn-success" type="button">Update</buttton>
                            </div>
                        </div>
                    </form>
                </div>
            </div>


        </div>
        <div class="container" v-else>
            <div class="card">
                <div class="card-header">

                    <h1>
                        <RouterLink to="/creategenderdata" class="btn btn-primary float-end">Add Gender</RouterLink>
                    </h1>
                    <div class="card-body">
                        <table class="table table-bordered">
                            <thead>
                                <tr>
                                    <th>Id</th>
                                    <th>Gender Name</th>
                                    <th>Code</th>
                                    <th>Action</th>
                                </tr>
                            </thead>
                            <tbody v-if="this.Employees.length > 0">
                                <tr v-for="(gender, index) in this.Employees" key="index">
                                    <td>{{ gender.Id }}</td>
                                    <td>{{ gender.GenderName }}</td>
                                    <td>{{ gender.Code }}</td>
                                    <td>
                                        <button type="button"  @click="getgender(gender.Id)"
                                            class="btn btn-success">Edit</button>

                                        <button type="button" @click="Deletegender(gender.Id)" class="btn btn-danger">Delete</button>
                                    </td>

                                </tr>
                            </tbody>
                            <tbody v-else>
                                <tr>
                                    <td colspan="4"> Loading...</td>
                                </tr>

                            </tbody>

                        </table>

                    </div>
                </div>


            </div>
        </div>
        


    </main>
</template>


<script>
import axios from 'axios'

export default {
    name: 'Employees',
    data() {
        return {
            Employees: [],
            Id: 0,
            gender: '',
            code: ''
        }
    },
    mounted() {
        console.log('Im came to vue.js'),
            this.getEmployee()
    },
    methods: {
        getEmployee() {

            axios.get('http://localhost:3000/Genders').then(res => {
                console.log(res)
                this.Employees = res.data
            })
        },
        getgender(gid) {
            this.Employees=[]
            var path = 'http://localhost:3000/Genders?id=' + gid
            axios.get(path).then(res => {
                console.log(res)
               
                this.Employees.push(res.data),
                this.Id=res.data.Id,
                this.gender=res.data.GenderName,
                this.code=res.data.Code

            })
        },
        UpdateGender(){
            const jsonObject ={"Id":this.Id,"GenderName":this.gender,"Code":this.code};
            console.log('Update method')
            console.log(jsonObject)

            axios.put('http://localhost:3000/Genders',jsonObject).then(res =>{
                console.log(res)
                this.Id=0
                this.gender='',
                this.code=''

            }).catch(error => {
          console.error('Error updating gender:', error);
        });
        this.getEmployee();
        },
        Deletegender(gid) {
            var path = 'http://localhost:3000/Genders?id=' + gid
            axios.get(path).then(res => {
                console.log(res)
               // this.Id=res.data.Id,
                //this.gender=res.data.GenderName,
                //this.code=res.data.Code

            })
            this.getEmployee();
        },
    }
}
</script>