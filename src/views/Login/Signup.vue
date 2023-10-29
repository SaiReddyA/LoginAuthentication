<template>
    <main>
        <h1>Sign Up Form</h1>




        <div v-if="this.Adddetails">
            <div class="container mt-5">
                <div class="card">
                    <div class="card-header">
                        <h1>Add UserDetails</h1>
                        <button style="float: right;" @click="toggleGet()" class="btn btn-primary" >Back to list</button>
                    </div>
                    <form>
                        <div class="card-body">

                            <div class="mb-3" v-if="this.update">
                                <lable>
                                   User Id
                                </lable>
                                <input type="text" class="form-control"   v-model="userid" name="gname" id="gname" />
                            </div>
                            <div class="mb-3">
                                <lable>
                                    UserName
                                </lable>
                                <input type="text" class="form-control" v-model="username" name="gname" id="gname" />
                            </div>
                            <div class="mb-3">
                                <lable>
                                    Occupation
                                </lable>
                                <input type="text" class="form-control" v-model="occupation" name="code" id="code" />
                            </div>
                            <div>
                                <buttton @click="UpdateSignup()" class="btn btn-success" type="button">Add</buttton>
                            </div>
                        </div>
                    </form>
                </div>
            </div>
        </div>
      
        <div class="container" v-if="this.getdetails">
            <div class="card">
                <div class="card-header">

                    <h1>
                        <RouterLink to="" @click="toggleAdd()"   class="btn btn-primary float-end">Sign Up</RouterLink>
                    </h1>
                    <div class="card-body">
                        <table class="table table-bordered">
                            <thead>
                                <tr class="table-primary">
                                    <th>User Id</th>
                                    <th>User Name</th>
                                    <th>Occupation</th>
                                    <th>Action</th>
                                </tr>
                            </thead>
                            <tbody v-if="this.Signup.length > 0">
                                <tr class="table-light" v-for="(signupkey, index) in this.Signup" key="index">
                                    <td >{{ signupkey.id }}</td>

                                    <td >{{ signupkey.UserName }}</td>
                                    <td >{{ signupkey.Occupation }}</td>
                                    <td>
                                        <button type="button"  @click="GetSignupbyId(signupkey.id)"
                                            class="btn btn-success">Edit</button>

                                        <button type="button" @click="DeleteSignup(signupkey.id)" class="btn btn-danger">Delete</button>
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
    name: 'Signin',
    data() {
        return {
            Signup: [],
            userid: null,
            username: '',
            occupation: '',
            update: false,
            getdetails: true,
        }
    },
    mounted() {
        console.log('Im came to vue.js'),
            this.GetSignupDetails()
    },
    methods: {
        GetSignupDetails() {
            this.Signup=[]
            axios.get('http://localhost:3000/Login').then(res => {
                console.log(res)
                this.Signup = res.data
            })
        },
        GetSignupbyId(gid) {
            this.update= true;
            this.getdetails = false
            this.Adddetails= true;
            var path = 'http://localhost:3000/Login?id=' + gid
            axios.get(path).then(res => {
                console.log(res)
               
                this.userid=res.data[0].id,
                this.username=res.data[0].UserName,
                this.occupation=res.data[0].Occupation

            })
        },
        UpdateSignup(){
            let jsonObject ={"UserName":this.username,"Occupation":this.occupation};
            console.log('Update method')
            console.log(jsonObject)
            if (this.userid) { // Check if this.userid is not empty or null
                  jsonObject = { "id": this.userid, "UserName": this.username, "Occupation": this.occupation };
                  let url = `http://localhost:3000/Login/${this.userid}`
                  axios.put(url,jsonObject).then(res =>{
                console.log(res)
                this.userid=null,
                this.username='',
                this.occupation=''

                    }).catch(error => {
                console.error('Error updating signup:', error);
                });
            }
           else {
             axios.post('http://localhost:3000/Login',jsonObject).then(res =>{
                console.log(res)
                this.userid=null,
                this.username='',
                this.occupation=''

                    }).catch(error => {
                console.error('Error updating signup:', error);
                });
           }
        this.GetSignupDetails();
        this.Adddetails= false;
        this.getdetails = true;
        window.location.reload();
        },
        DeleteSignup(gid) {
            var path = `http://localhost:3000/Login/${gid}`; 
            axios.delete(path).then(res => {
                console.log(res)
                this.userid=res.data.userid,
                this.username=res.data.username,
                this.occupation=res.data.occupation

            })
            this.GetSignupDetails();
        },
        
        toggleGet(){
        this.update= false;
        this.Adddetails = false
        this.getdetails = true

        },
        toggleAdd(){
        this.update= false;
        this.Adddetails = true
        this.getdetails = false
        }
    }
}
</script>