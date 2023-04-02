import {
    AspectRatio,
    Button,
    ButtonGroup,
    Card,
    CardBody,
    CardFooter,
    Divider,
    Heading,
    Image,
    Modal,
    ModalBody,
    ModalCloseButton,
    ModalContent,
    ModalFooter,
    ModalHeader,
    ModalOverlay,
    Stack,
    Table,
    Tbody,
    Td,
    Text,
    Tfoot,
    Th,
    Thead,
    Tr,
    useDisclosure,
    useToast,
} from '@chakra-ui/react'
import NextLink from 'next/link'
import { useRouter } from 'next/router';
import { useState } from 'react'
import { useAppStates } from '../lib/AppContext'

function ConfirmOrderModal({ checkedItems, ...props }) {
    const { user, cart, api, action } = useAppStates();
    const { isOpen, onOpen, onClose } = useDisclosure();
    const [loading, setLoading] = useState(false);
    const toast = useToast();
    const router = useRouter()
    const [orderCart, setOrderCart] = useState(() => checkedItems.map(mealId => ({ ...cart.find(x => x.MealId == mealId) })));
    const prices = orderCart.map(e => e.Amount * e.MealPrice)

    function ready() {
        onOpen()
        setOrderCart(checkedItems.map(mealId => ({ ...cart.find(x => x.MealId == mealId) })))
    }

    async function order() {
        try {
            console.log(orderCart)
            setLoading(true)
            await api.createOrder(orderCart);
            await action.getUserCart()
            router.push("/orders")
        } catch (e) {
            console.error(e)
            toast({
                title: `${e.response.data.Message}` || "There is an error occured!",
                status: "error"
            })
        } finally {
            setLoading(false)
            onClose()
        }
    }

    return (
        <>
            <Button size="lg" colorScheme="purple" onClick={ready}>Order</Button>
            <Modal isOpen={isOpen} onClose={onClose} size={{ base: "full", md: "md" }} scrollBehavior="inside">
                <ModalOverlay />
                <ModalContent>
                    <ModalHeader>Confirm Your Order</ModalHeader>
                    <ModalCloseButton display={{ base: "none", md: "block" }} />
                    <ModalBody>
                        <Table>
                            <Thead>
                                <Tr>
                                    <Th>Meals</Th>
                                    <Th isNumeric>Amount</Th>
                                    <Th isNumeric>Price</Th>
                                </Tr>
                            </Thead>
                            <Tbody>
                                {orderCart.map((e, i) => (
                                    <Tr key={e.MealId}>
                                        <Td>{e.MealName}</Td>
                                        <Td isNumeric>{e.Amount}</Td>
                                        <Td isNumeric>{e.MealPrice}</Td>
                                    </Tr>
                                ))}
                            </Tbody>
                            <Tfoot>
                                <Tr>
                                    <Th>Total</Th>
                                    <Th isNumeric></Th>
                                    <Th isNumeric>
                                        {
                                            prices.reduce(
                                                (a, b) => a + b,
                                                0
                                            )
                                        }
                                    </Th>
                                </Tr>
                            </Tfoot>
                        </Table>
                    </ModalBody>
                    <ModalFooter>
                        <Button variant="ghost" onClick={onClose} display={{ base: "block", md: "none" }}>
                            Close
                        </Button>
                        <Button colorScheme='blue' onClick={order} isLoading={loading}>
                            Order
                        </Button>
                    </ModalFooter>
                </ModalContent>
            </Modal>
        </>
    )
}

export default ConfirmOrderModal
