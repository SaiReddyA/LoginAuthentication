<template>
    <main>
        <div class="container mt-5">
        <div class="card">
            <div class="card-header">
                <h1>Add Gender</h1>
                <RouterLink style="float: end;" to="/employee">Back to list</RouterLink>
            </div>
           <form > 
                <div class="card-body">

                    <div class="mb-3">
                        <lable>
                            GenderName
                        </lable>
                        <input type="text"  class="form-control" v-model="gender"   name="gname" id="gname"/>
                    </div>
                    <div class="mb-3">
                        <lable>
                            Code
                        </lable>
                        <input type="text" class="form-control"  v-model="code"  name="code" id="code" />
                    </div>
                    <div>
                        <buttton @click="saveGender()"  class="btn btn-primary" type="button">Save</buttton>
                    </div>
                </div>
           </form>
        </div>
    </div>
    </main>
</template>
<script>

import axios from 'axios'

export default{
   name:'CreateGender',
    Data(){
        return{
          genderdata:{
                gendername: '',
                code: ''
            },
            gender:'',
            code:''
        }
        
    },
    
    mounted(){
        console.log('Im in create vue.js')
      
        console.log('sai')
    },
    methods: {
        saveGender(){
            const jsonObject ={"GenderName":this.gender,"Code":this.code};
            console.log('save method')
            console.log(jsonObject)

            axios.post('http://localhost:3000/Genders',jsonObject).then(res =>{
                console.log(res)
                this.gender='',
                this.code=''

            }).catch(error => {
          console.error('Error creating gender:', error);
        });
        
        },
       
    }
}


  
</script>
