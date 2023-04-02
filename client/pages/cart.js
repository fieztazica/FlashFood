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
import { useEffect, useState } from 'react'
import NextLink from 'next/link'
import ConfirmOrderModal from '../components/ConfirmOrderModal'

function Cart() {
    const { user, cart, action } = useAppStates()
    const [checkedItems, setCheckedItems] = useState(cart.map(i => i.MealId))

    useEffect(() => {
        if (!!cart.length && !checkedItems.length)
            setCheckedItems(cart.map(i => i.MealId))
    }, [cart])

    if (!user)
        return (
            <Center minH="2xl" w="full" py={5} justifyItems="center">
                <NextLink href="/login">
                    <Button size="lg" colorScheme="purple">Please login first</Button>
                </NextLink>
            </Center>
        );

    if (!cart.length)
        return (
            <Center minH="2xl" w="full" py={5} justifyItems="center">
                No items found.
            </Center>
        );

    const allChecked = !!checkedItems.length && cart.every((ele) => checkedItems.includes(ele.MealId))
    const isIndeterminate = cart.some((ele) => checkedItems.includes(ele.MealId) == true) && !allChecked

    function onAmountChange(MealId, amount) {
        action.setCart(arr => {
            const temp = [...arr]
            const item = temp.find(x => x.MealId == MealId)

            if (!!amount && amount > 0)
                item.Amount = amount
            else
                item.Amount = 1

            return temp;
        })
    }

    function incAmount(MealId) {
        action.setCart(arr => {
            const newArr = arr.map((item, index) => {
                if (item.MealId === MealId)
                    return { ...item, Amount: item.Amount + 1 }
                return item;
            })
            return newArr;
        })
    }

    function decAmount(MealId) {
        action.setCart(arr => {
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
                    onChange={(e) => setCheckedItems(e.target.checked ? cart.map(i => i.MealId) : [])}
                >
                    Select All
                </Checkbox>
                <Spacer />
                <ConfirmOrderModal checkedItems={checkedItems} />
            </Flex>
            <VStack>
                {cart.map((item) => (
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
                                        isDisabled={!checkedItems.includes(item.MealId) || item.Amount <= 1}
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
                                <Button width="full" colorScheme="red" onClick={() => action.deleteCartItem(item)}>
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
                    onChange={(e) => setCheckedItems(e.target.checked ? cart.map(i => i.MealId) : [])}
                >
                    Select All
                </Checkbox>
                <Spacer />
                <ConfirmOrderModal checkedItems={checkedItems} />
            </Flex>
        </Box>
    )
}

Cart.getLayout = function getLayout(page) {
    return <AppLayout>{page}</AppLayout>
}

export default Cart
