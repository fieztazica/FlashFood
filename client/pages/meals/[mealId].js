import AppLayout from '@/components/layouts/appLayout'
import {
    Box,
    Button,
    Container,
    Grid,
    Heading,
    Text,
    Image,
    GridItem,
} from '@chakra-ui/react'
function Shop() {
    return (
        <>
            <Box></Box>
        </>
    )
}
Shop.getLayout = function getLayout(page) {
    return <AppLayout bg={'red'}>{page}</AppLayout>
}

export default Shop
