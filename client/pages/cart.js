import Head from 'next/head'
import AppLayout from '@/components/layouts/appLayout'
import {
    Box,
    Button,
    Heading,
    Image,
    Card,
    Stack,
    CardBody,
    CardFooter,
    Text,
    Checkbox,
    Flex,
    Spacer,
    HStack,
    Input,
    Center,
    VStack,
} from '@chakra-ui/react'
import { useAppStates } from '../lib/AppContext'
import { useState } from 'react'

const listItem = [
    {
        MealId: 1,
        UserId: 'avb',
        Amount: 20,
        MealName: 'Vien Chien',
        MealImageURL: '',
    },
    {
        MealId: 2,
        UserId: 'aasvb',
        Amount: 22,
        MealName: 'Vien Chien',
        MealImageURL: '',
    },
]

function Cart() {
    const { user, cart } = useAppStates()
    const [fakeCart, setFakeCart] = useState([...listItem])
    const [checkedItems, setCheckedItems] = useState(fakeCart.map(i => i.MealId))
    const [submiting, setSubmiting] = useState(false)

    if (!fakeCart.length)
        return (
            <Center minH="2xl" w="full" py={5} justifyItems="center">
                No items found.
            </Center>
        );

    const allChecked = !!checkedItems.length && fakeCart.every((ele) => checkedItems.includes(ele.MealId))
    const isIndeterminate = fakeCart.some((ele) => checkedItems.includes(ele.MealId) == true) && !allChecked

    function onAmountChange(MealId, amount) {
        setFakeCart(arr => {
            const temp = [...arr]
            const item = temp.find(x => x.MealId == MealId)
            item.Amount = amount
            return temp;
        })
    }

    function incAmount(MealId) {
        setFakeCart(arr => {
            const newArr = arr.map((item, index) => {
                if (item.MealId === MealId)
                    return { ...item, Amount: item.Amount + 1 }
                return item;
            })
            return newArr;
        })
    }

    function decAmount(MealId) {
        setFakeCart(arr => {
            const newArr = arr.map((item, index) => {
                if (item.MealId === MealId)
                    return { ...item, Amount: item.Amount - 1 }
                return item;
            })
            return newArr;
        })
    }

    return (
        <Box>
            <Flex padding={5}>
                <Checkbox
                    size={'lg'}
                    isChecked={allChecked}
                    isIndeterminate={isIndeterminate}
                    onChange={(e) => setCheckedItems(e.target.checked ? fakeCart.map(i => i.MealId) : [])}
                >
                    Select All
                </Checkbox>
                <Spacer />
                <Button size="lg" colorScheme="purple">Order</Button>
            </Flex>
            <VStack>
                {fakeCart.map((item) => (
                    <Card
                        direction={{ base: 'column', sm: 'row' }}
                        overflow="hidden"
                        variant="outline"
                        width="full"
                        key={item.MealId}
                    >
                        <Image
                            objectFit="cover"
                            maxW={{ base: '100%', sm: '200px' }}
                            src={item.MealImageURL}
                            alt={item.MealName}
                        />
                        <CardBody>
                            <Stack>
                                <Heading size="md">
                                    {item.MealName}
                                </Heading>

                                <Text py="2">
                                    Caffè latte is a coffee beverage of
                                    Italian origin made with espresso
                                    and steamed milk.
                                </Text>
                            </Stack>
                        </CardBody>
                        <CardFooter>
                            <VStack width={{ base: "full", md: "fit-content" }}>
                                <Flex width="full" flexDirection={{ base: "row", md: "row-reverse" }} justify={{ base: "left", md: "right" }}>
                                    <Checkbox
                                        size="lg"
                                        isChecked={checkedItems.includes(item.MealId)}
                                        onChange={(e) =>
                                            setCheckedItems((arr) =>
                                                e.target.checked
                                                    ? [...arr, item.MealId]
                                                    : arr.filter(x => x != item.MealId)
                                            )
                                        }
                                    />
                                    <Text mx={2}>Select</Text>
                                </Flex>
                                <HStack maxW={{ base: "full", md: "150px" }}>
                                    <Button
                                        isDisabled={!checkedItems.includes(item.MealId) || item.Amount <= 0}
                                        onClick={() => decAmount(item.MealId)}
                                    >-</Button>
                                    <Input
                                        value={item.Amount}
                                        onChange={(e) => onAmountChange(item.MealId, e.target.value)}
                                        isDisabled={!checkedItems.includes(item.MealId)}
                                        width="100%"
                                    />
                                    <Button
                                        isDisabled={!checkedItems.includes(item.MealId)}
                                        onClick={() => incAmount(item.MealId)}
                                    >+</Button>
                                </HStack>
                                <Button width="full" colorScheme="red">
                                    Delete
                                </Button>
                            </VStack>
                        </CardFooter>
                    </Card>
                ))}
            </VStack>
            <Flex padding={5}>
                <Checkbox
                    size={'lg'}
                    isChecked={allChecked}
                    isIndeterminate={isIndeterminate}
                    onChange={(e) => setCheckedItems(e.target.checked ? fakeCart.map(i => i.MealId) : [])}
                >
                    Select All
                </Checkbox>
                <Spacer />
                <Button size="lg" colorScheme="purple">Order</Button>
            </Flex>
        </Box>
    )
}

Cart.getLayout = function getLayout(page) {
    return <AppLayout>{page}</AppLayout>
}

export default Cart
