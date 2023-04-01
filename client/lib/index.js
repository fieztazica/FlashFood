export const tokenKey = "flashfood_token";

export const controllers = {
    account: `/api/Account`,
    meal: `/api/Meals`,
    order: `/api/Orders`,
    cartitem: `/api/CartItems`,
}

/**
 *
 * @param {import("axios").AxiosInstance} instance
 * @returns
 */
export default function api(instance) {
    instance.defaults.headers.post["Content-Type"] = 'application/json';

    const login = async ({ email, password, rememberMe = false, ...props }) => {
        const { data } = await instance.post(`${controllers.account}/Login`, {
            "Email": email,
            "Password": password
        });
        const token = `${data}`

        localStorage.setItem(tokenKey, token);
        setTokenToInstance(token)
    }

    function setTokenToInstance(token) {
        instance.defaults.headers.common['Authorization'] = `Bearer ${token}`
    }

    function clearToken() {
        delete instance.defaults.headers.common['Authorization']
    }

    const register = async (body) => {
        const res = await instance.post(`${controllers.account}/Register`, body);
    }

    const logout = () => {
        localStorage.removeItem(tokenKey);
        clearToken();
    }

    const getUserInfo = async () => {
        const { data } = await instance.get(`${controllers.account}/UserInfo`)
        return data;
    }

    const addToCart = async (item, amount = 1) => {
        const cartItem = {
            MealId: item.Id,
            Amount: amount
        }
        const { data } = await instance.post(`${controllers.cartitem}/Create`, cartItem)
        return data;
    }

    const getCart = async () => {
        const { data } = await instance.get(`${controllers.account}/GetMine`)
        return data;
    }

    const deleteCartItem = async (id) => {
        const { data } = await instance.delete(`${controllers.account}/Delete`, {
            MealId: id
        })
        return data;
    }

    /**
     * 
     * @param {number} id
     * @returns
     */
    const getMeal = async (id) => {
        const { data } = await instance.get(`${controllers.meal}/Get/${id}`);
        return data;
    }

    /**
     * 
     * @param {number} pageNumber
     * @param {number} pageSize
     * @returns
     */
    const getMeals = async (pageNumber = 1, pageSize = 10) => {
        const { data } = await instance.get(`${controllers.meal}/Get?pageNumber=${pageNumber}&pageSize=${pageSize}`);
        return data;
    }

    /**
     * 
     * @param {number} pageNumber
     * @param {number} pageSize
     * @returns
     */
    const getOrders = async (pageNumber = 1, pageSize = 10) => {
        const { data } = await instance.get(`${controllers.order}/Get?pageNumber=${pageNumber}&pageSize=${pageSize}`);
        return data;
    }

    /**
     * 
     * @param {number} id
     * @returns
     */
    const getOrder = async (id) => {
        const { data } = await instance.get(`${controllers.order}/Get/${id}`);
        return data;
    }

    const createOrder = async (items) => {
        const orderBody = {
            ListCart: items
        }
        const { data } = await instance.post(`${controllers.order}/Create`, orderBody);
        return data;
    }

    const cancelOrder = async (id) => {
        const { data } = await instance.post(`${controllers.order}/UpdateStatus/${id}?status=canceled`);
        return data;
    }

    return {
        // Auth
        login,
        getUserInfo,
        register,
        logout,
        clearToken,
        setTokenToInstance,
        // Order
        getOrders,
        getOrder,
        createOrder,
        cancelOrder,
        // Meal
        getMeal,
        getMeals,
        // Cart
        getCart,
        addToCart,
        deleteCartItem,
        // Misc
    }
}
