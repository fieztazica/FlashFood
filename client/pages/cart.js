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
    useNumberInput,
    Input,
} from '@chakra-ui/react'
import { useAppStates } from '../lib/AppContext'
import Item from '../components/Item'
import React from 'react'

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
    const [checkedItems, setCheckedItems] = React.useState(() =>
        cart.map((i) => false)
    )
    const allChecked = checkedItems.every((i) => i === true)
    const isIndeterminate = checkedItems.some((i) => i === true) && !allChecked
    const { getInputProps, getIncrementButtonProps, getDecrementButtonProps } =
        useNumberInput({
            step: 1,
            defaultValue: 1,
            min: 1,
            max: 6,
        })
    const inc = getIncrementButtonProps()
    const dec = getDecrementButtonProps()
    const input = getInputProps()

    return (
        <Box>
            {!listItem.length ? (
                <Text>Your cart is empty!</Text>
            ) : (
                <>
                    <Flex padding={5}>
                        <Checkbox
                            size={'lg'}
                            isChecked={allChecked}
                            isIndeterminate={isIndeterminate}
                            onChange={(e) =>
                                setCheckedItems((a) =>
                                    new Array(a.length).fill(e.target.checked)
                                )
                            }
                        >
                            Select All
                        </Checkbox>
                        <Spacer />
                        <Button>Buy</Button>
                    </Flex>
                    {listItem.map((item, index) => (
                        <Flex>
                            <Card
                                direction={{ base: 'column', sm: 'row' }}
                                overflow="hidden"
                                variant="outline"
                                width="full"
                                key={index}
                            >
                                <Image
                                    objectFit="cover"
                                    maxW={{ base: '100%', sm: '200px' }}
                                    src="https://images.unsplash.com/photo-1667489022797-ab608913feeb?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxlZGl0b3JpYWwtZmVlZHw5fHx8ZW58MHx8fHw%3D&auto=format&fit=crop&w=800&q=60"
                                    alt="Caffe Latte"
                                />
                                <Stack>
                                    <CardBody>
                                        <Heading size="md">
                                            The perfect latte
                                        </Heading>

                                        <Text py="2">
                                            Caffè latte is a coffee beverage of
                                            Italian origin made with espresso
                                            and steamed milk.
                                        </Text>
                                    </CardBody>
                                    <CardFooter>
                                        <Box>
                                            <HStack maxW="150px">
                                                <Button {...dec}>-</Button>
                                                <Input
                                                    {...input}
                                                    textAlign={'center'}
                                                />
                                                <Button {...inc}>+</Button>
                                            </HStack>
                                        </Box>
                                    </CardFooter>
                                </Stack>
                                <Spacer />
                                <Checkbox
                                    size="lg"
                                    isChecked={checkedItems[index]}
                                    onChange={(e) =>
                                        setCheckedItems((arr) =>
                                            arr.map((ele, i) => {
                                                if (i === index) {
                                                    return e.target.checked
                                                }
                                                return ele
                                            })
                                        )
                                    }
                                />
                            </Card>
                        </Flex>
                    ))}
                </>
            )}
            <Flex padding={5}>
                <Checkbox
                    size={'lg'}
                    isChecked={allChecked}
                    isIndeterminate={isIndeterminate}
                    onChange={(e) =>
                        setCheckedItems((a) =>
                            new Array(a.length).fill(e.target.checked)
                        )
                    }
                >
                    Select All
                </Checkbox>
                <Spacer />
                <Button>Buy</Button>
            </Flex>
        </Box>
    )
}

Cart.getLayout = function getLayout(page) {
    return <AppLayout>{page}</AppLayout>
}

export default Cart
