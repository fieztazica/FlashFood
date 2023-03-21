const { default: axios } = require('axios')

const baseUrl = process.env.BACKEND_URL
const defaultApiEndpoint = `${baseUrl}/api`
const accountController = `${defaultApiEndpoint}/Account`

const login = async (email, password, rememberMe) => {
    const res = await axios.post(`${accountController}/Login`).then(r => r.data);
    localStorage.setItem("flashfood_token")
}

const register = () => {}

const getUserInfo = () => {
    
}
