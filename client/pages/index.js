import Head from 'next/head'
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
    useDisclosure,
    SimpleGrid,
} from '@chakra-ui/react'
import { useAppStates } from '../lib/AppContext'
import Item from '../components/Item'

function Home() {
    const { user } = useAppStates()
    const { isOpen, onOpen, onClose, onToggle } = useDisclosure()
    return (

        <Box>
            <Box id="san pham noi bat" py={5}>
                <Heading size={"md"} mb={2 }>
                San pham Noi bat
                </Heading>
                <SimpleGrid columns={[2, null, 4]} spacing={5}>
                    {[1, 2, 3, 4, 5, 6, 7, 8, 9, 10].map((num, i) => <Item />)}
                </SimpleGrid>
            </Box>
        </Box>

    )
}

Home.getLayout = function getLayout(page) {
    return <AppLayout>{page}</AppLayout>
}

export default Home
