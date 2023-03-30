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
    Stack,
    Text,
} from '@chakra-ui/react'
import NextLink from 'next/link'

function Item({ obj, ...props }) {
    return (
        <Card
            as={NextLink}
            href={`/meals/${obj["Id"]}`}
            maxW="sm"
            transitionDuration={'0.3s'}
            _hover={{ boxShadow: '1px 1px 1px 1px grey' }}
            {...props}
        >
            <CardBody>
                <AspectRatio ratio={1}>
                    <Image
                        src={obj["ImageURL"]}
                        alt="Green double couch with wooden legs"
                        borderRadius="lg"
                        objectFit='cover'
                    />
                </AspectRatio>
                <Stack mt="6">
                    <Heading size="md">{obj['Name']}</Heading>
                    <Text color="blue.600" fontSize={['md', 'xl']}>
                        VND {obj['Price']}
                    </Text>
                </Stack>
            </CardBody>
        </Card>
    )
}

export default Item
