import AppLayout from '@/components/layouts/appLayout'
import { Box, Button, Heading, Image } from '@chakra-ui/react'
import { useAppStates } from '../../lib/AppContext';

function Orders() {
    const { user } = useAppStates();
    return (
        <>
            <Box>
            
            </Box>
        </>
    )
}

Orders.getLayout = function getLayout(page) {
    return (
        <AppLayout>
            {page}
        </AppLayout>
    )
}

export default Orders
