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
    Thead,
    Tr,
    Th,
    Td,
    Table,
    Spinner,
    HStack,
    Spacer,
    useToast,
} from '@chakra-ui/react'
import { useRouter } from 'next/router'
import { useAppStates } from '../../lib/AppContext'
import NextLink from 'next/link'
import { useEffect, useState } from 'react'

function Order() {
    const { user, api } = useAppStates()
    const [order, setOrder] = useState(null)
    const [loading, setLoading] = useState(false)
    const router = useRouter()
    const toast = useToast()
    const [canceling, setCanceling] = useState(false)

    useEffect(() => {
        if (user) {
            fetchData()
        }
    }, [user])

    async function fetchData() {
        try {
            setLoading(true)
            const data = await api.getOrder(router.query.orderId);
            if (data) setOrder(data)
        } catch (e) {
            console.error(e)
        } finally {
            setLoading(false)
        }

    }

    async function cancelOrder() {
        try {
            setCanceling(true)
            await api.cancelOrder(order.Id)
            await fetchData()
        } catch (e) {
            toast({
                status: "error",
                title: `${e.response.data.Message}` || 'There is an error occured!'
            })
            console.error(e)
        } finally {
            setCanceling(false)
        }
    }

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

    if (!!loading)
        return (
            <Center minH="2xl" w="full" py={5} justifyItems="center">
                <Spinner />
            </Center>
        );


    if (!loading && !order)
        return (
            <Center minH="2xl" w="full" py={5} justifyItems="center">
                Order not found.
            </Center>
        );

    return (
        <>
            <Box minH="70vh" py={5}>
                <Flex justify="space-between">
                    <HStack mb={2}>
                        <Button variant="ghost" as={NextLink} href="/orders">Orders</Button>
                        <Heading >
                            / Order #{order.Id}
                        </Heading>
                    </HStack>
                    {(order.Status == "ordered" || order.Status == "canceled") &&
                        <Button
                            colorScheme="red"
                            variant="ghost"
                            isDisabled={order.Status == "canceled"}
                            isLoading={canceling}
                            onClick={() => cancelOrder()}
                        >
                            {order.Status == "canceled" ? "Canceled" : "Cancel"}
                        </Button>
                    }
                </Flex>
                <Divider />
                <Box p={5}>
                    <Text fontWeight="semibold">Total: {order.TotalMoney}</Text>
                    <Text>Status: {order.Status}</Text>
                    {order.Paid && <Text>Paid {order.Paid} at {order.PaidAt}</Text>}
                    <Text>Order placed at {new Date(order.OrderAt).toLocaleString()}</Text>
                    {order.Change && <Text>Change: {order.Change}</Text>}
                </Box>
                <Divider />
                <Table>
                    <Thead>
                        <Tr>
                            <Th>Meals</Th>
                            <Th isNumeric>Amount</Th>
                            <Th isNumeric>Price</Th>
                        </Tr>
                    </Thead>
                    {order.Items.map((e, i) => (
                        <Tbody key={e.id}>
                            <Td>{e.MealName}</Td>
                            <Td isNumeric>{e.Amount}</Td>
                            <Td isNumeric>{e.MealPrice}</Td>
                        </Tbody>
                    ))}
                </Table>
            </Box>
        </>
    )
}

Order.getLayout = function getLayout(page) {
    return <AppLayout title={'Your Orders'}>{page}</AppLayout>
}

export default Order
