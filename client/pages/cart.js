import AppLayout from '@/components/layouts/appLayout'
import React from 'react'
function Cart() {
    return <div>cart</div>
}
Cart.getLayout = function getLayout(page) {
    return <AppLayout bg={'red'}>{page}</AppLayout>
}

export default Cart
