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
    Card,
    CardBody,
    CardHeader,
    Stack,
    CardFooter,
    Divider,
    Flex,
    ButtonGroup,
    AspectRatio,
    Center,
    Tbody,
} from '@chakra-ui/react'
import { useRouter } from 'next/router'
import { useAppStates } from '../../lib/AppContext'
import NextLink from 'next/link'
import { useEffect, useState } from 'react'

const fakeTableRows = new Array(10).fill(
    {
        id: 1,
        meal: 'ca vien chien',
        amount: 10,
        total: 45000,
    },
    {
        id: 2,
        meal: 'ca vien chien',
        amount: 10,
        total: 45000,
    },
    {
        id: 3,
        meal: 'ca vien chien',
        amount: 10,
        total: 45000,
    },
    {
        id: 4,
        meal: 'ca vien chien',
        amount: 10,
        total: 45000,
    },
    {
        id: 5,
        meal: 'ca vien chien',
        amount: 10,
        total: 45000,
    },
    {
        id: 6,
        meal: 'ca vien chien',
        amount: 10,
        total: 45000,
    },
    {
        id: 7,
        meal: 'ca vien chien',
        amount: 10,
        total: 45000,
    },
    {
        id: 8,
        meal: 'ca vien chien',
        amount: 10,
        total: 45000,
    },
    {
        id: 9,
        meal: 'ca vien chien',
        amount: 10,
        total: 45000,
    },
    {
        id: 10,
        meal: 'ca vien chien',
        amount: 10,
        total: 45000,
    }
)

function Order() {
    const { user, api } = useAppStates()
    const [redirectUrl, setRedirectUrl] = useState(null)
    const router = useRouter()

    if (!api || !user)
        return (
            <Center minH="2xl" w="full" py={5} justifyItems="center">
                <NextLink href="/login">
                    <Button size="lg" colorScheme="purple">
                        Please login first
                    </Button>
                </NextLink>
            </Center>
        )

    useEffect(() => {}, [])

    return (
        <>
            <Box minH="70vh">
                <Thead>
                    <Tr>
                        <Th>Meals</Th>
                        <Th>Amount</Th>
                        <Th isNumeric>Total</Th>
                    </Tr>
                </Thead>
                {fakeTableRows.map((e, i) => (
                    <Tbody key={e.id}>
                        <td>{e.meal}</td>
                        <td>{e.amount}</td>
                        <td isNumeric>{e.total}</td>
                    </Tbody>
                )}
            </Box>
        </>
    )
}

Order.getLayout = function getLayout(page) {
    return <AppLayout title={'Your Orders'}>{page}</AppLayout>
}

export default Order
