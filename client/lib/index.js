const { default: axios } = require('axios')

const baseUrl = `${process.env.apiBaseUrl}`
const defaultApiEndpoint = `${baseUrl}/api`
const tokenKey = "flashfood_token";
const getToken = () => localStorage.getItem(tokenKey);

const controllers = {
    account: `${defaultApiEndpoint}/Account`,
}

/**
 * @param {string} email
 * @param {string} password
 * @param {boolean} rememberMe
 */
const login = async (email, password, rememberMe = false) => {
    const token = await axios.post(`${controllers.account}/Login`, {
        "Email": email,
        "Password": password
    }, {
        headers: {
            "Content-Type": "application/json"
        }
    }).then(r => r.data);
    console.log(token);
    localStorage.setItem(tokenKey, `${token}`);
}

const register = () => {

}

const getUserInfo = async () => {
    const token = getToken();
    const res = await axios.get()
}

export {
    login,
    getToken,
    register,
    getUserInfo,
    controllers
}
